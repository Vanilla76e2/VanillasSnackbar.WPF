using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Windows;
using VanillasSnackbar.WPF.Controls;
using VanillasSnackbar.WPF.Models;

namespace VanillasSnackbar.WPF.Managers
{
    public class SnackbarManager
    {
        public ObservableCollection<SnackbarControl> Messages { get; } = new();
        public SnackbarMode Mode { get; set; } // теперь можно менять режим

        private readonly ConcurrentQueue<SnackbarMessage> _singleQueue = new();
        private bool _isDisplayingSingle;

        public SnackbarManager(SnackbarMode mode = SnackbarMode.Multiple)
        {
            Mode = mode;
        }

        public void ShowMessage(SnackbarMessage message)
        {
            if (Mode == SnackbarMode.Multiple)
            {
                AddMessageMultiple(message);
            }
            else
            {
                _singleQueue.Enqueue(message);
                if (!_isDisplayingSingle)
                    DisplayNextSingleMessage();
            }
        }

        #region Multiple mode

        private void AddMessageMultiple(SnackbarMessage message)
        {
            var control = new SnackbarControl { Message = message };
            control.Closed += RemoveMessage;

            // вставляем в начало коллекции, чтобы новые сообщения были сверху
            Messages.Insert(0, control);

            _ = Task.Delay(message.Duration).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (Messages.Contains(control))
                        control.Close();
                });
            });
        }

        #endregion

        #region Single mode

        private void DisplayNextSingleMessage()
        {
            if (!_singleQueue.TryDequeue(out var message))
            {
                _isDisplayingSingle = false;
                return;
            }

            _isDisplayingSingle = true;

            var control = new SnackbarControl { Message = message };
            control.Closed += OnSingleControlClosed;

            Messages.Clear();
            Messages.Insert(0, control);

            _ = Task.Delay(message.Duration).ContinueWith(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (Messages.Contains(control))
                        control.Close();
                });
            });
        }

        private void OnSingleControlClosed(SnackbarControl control)
        {
            control.Closed -= OnSingleControlClosed;
            Messages.Remove(control);
            _isDisplayingSingle = false;
            DisplayNextSingleMessage();
        }

        #endregion

        private void RemoveMessage(SnackbarControl control)
        {
            control.Closed -= RemoveMessage;
            if (Messages.Contains(control))
                Messages.Remove(control);
        }
    }
}
