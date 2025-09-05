using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using VanillasSnackbar.WPF.Models;

namespace VanillasSnackbar.WPF.Controls
{
    public class SnackbarControl : Control
    {
        static SnackbarControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SnackbarControl),
                new FrameworkPropertyMetadata(typeof(SnackbarControl)));
        }

        #region DependencyProperties

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(
                nameof(Message),
                typeof(SnackbarMessage),
                typeof(SnackbarControl),
                new PropertyMetadata(null, OnMessageChanged));

        public SnackbarMessage Message
        {
            get => (SnackbarMessage)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register(
                nameof(CloseCommand),
                typeof(ICommand),
                typeof(SnackbarControl),
                new PropertyMetadata(null));

        public ICommand CloseCommand
        {
            get => (ICommand)GetValue(CloseCommandProperty);
            set => SetValue(CloseCommandProperty, value);
        }

        #endregion

        private DispatcherTimer? _timer;
        private FrameworkElement? _timerBar;

        public SnackbarControl()
        {
            CloseCommand = new RelayCommand(_ => OnClose());
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _timerBar = GetTemplateChild("PART_TimerBar") as FrameworkElement;

            if (_timerBar != null && Message != null)
            {
                // Чтобы ActualWidth был корректным
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    StartTimerBarAnimation(Message.Duration);
                }), DispatcherPriority.Loaded);
            }
        }

        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SnackbarControl control && e.NewValue is SnackbarMessage msg)
            {
                control.StartTimer(msg.Duration);

                if (control._timerBar != null)
                    control.StartTimerBarAnimation(msg.Duration);
            }
        }

        private void StartTimer(TimeSpan duration)
        {
            _timer?.Stop();
            _timer = new DispatcherTimer { Interval = duration };
            _timer.Tick += (_, _) =>
            {
                _timer.Stop();
                OnClose();
            };
            _timer.Start();
        }

        private void StartTimerBarAnimation(TimeSpan duration)
        {
            if (_timerBar == null) return;

            double fullWidth = _timerBar.ActualWidth;
            if (fullWidth <= 0) fullWidth = 100; // fallback

            var anim = new DoubleAnimation
            {
                From = fullWidth,
                To = 0,
                Duration = new Duration(duration),
                FillBehavior = FillBehavior.Stop
            };
            _timerBar.BeginAnimation(FrameworkElement.WidthProperty, anim);
        }

        public event Action<SnackbarControl>? Closed;

        public void Close()
        {
            OnClose();
        }

        private void OnClose()
        {
            Closed?.Invoke(this);
        }
    }

    // Простая RelayCommand
    internal class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        public event EventHandler? CanExecuteChanged;
        public RelayCommand(Action<object?> execute) => _execute = execute;
        public bool CanExecute(object? parameter) => true;
        public void Execute(object? parameter) => _execute(parameter);
    }
}
