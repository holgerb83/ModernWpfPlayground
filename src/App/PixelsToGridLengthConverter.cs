using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ModernWpfPlayground
{
    public class PixelsToGridLengthConverter : MarkupExtension, IValueConverter
    {
        private static PixelsToGridLengthConverter? _converter;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is double d ? new GridLength(d) : new GridLength(1.0, GridUnitType.Auto);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) =>
            _converter ??= new PixelsToGridLengthConverter();
    }
}