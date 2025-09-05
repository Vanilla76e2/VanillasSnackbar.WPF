using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using VanillasSnackbar.WPF.Models;

namespace VanillasSnackbar.WPF.Converters
{
    internal class SnackbarTypeToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not SnackbarType type)
                return Brushes.Gray;

            return type switch
            {
                SnackbarType.Info => (Brush)Application.Current.Resources["SnackbarInfoBrush"],
                SnackbarType.Success => (Brush)Application.Current.Resources["SnackbarSuccessBrush"],
                SnackbarType.Warning => (Brush)Application.Current.Resources["SnackbarWarningBrush"],
                SnackbarType.Error => (Brush)Application.Current.Resources["SnackbarErrorBrush"],
                _ => Brushes.Gray
            };
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
