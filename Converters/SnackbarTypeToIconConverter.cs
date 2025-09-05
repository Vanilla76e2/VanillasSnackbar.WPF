using System.Globalization;
using System.Windows.Data;
using VanillasSnackbar.WPF.Models;

namespace VanillasSnackbar.WPF.Converters
{
    class SnackbarTypeToIconConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SnackbarType type)
            {
                return type switch
                {
                    SnackbarType.Info => "ℹ",
                    SnackbarType.Success => "✔",
                    SnackbarType.Warning => "⚠",
                    SnackbarType.Error => "✖",
                    _ => ""
                };
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
