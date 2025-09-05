using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanillasSnackbar.WPF.Models
{
    public enum SnackbarType
    {
        Info,
        Success,
        Warning,
        Error
    }

    public class SnackbarMessage
    {
        public string Text { get; set; }
        public SnackbarType Type { get; set; }
        public TimeSpan Duration { get; set; }

        public SnackbarMessage(string text, SnackbarType type = SnackbarType.Info, TimeSpan? duration = null)
        {
            Text = text;
            Type = type;
            Duration = duration ?? TimeSpan.FromSeconds(3);
        }
    }
}
