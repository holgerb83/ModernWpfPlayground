using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;
using FontFamily = System.Windows.Media.FontFamily;
using Point = System.Windows.Point;

namespace ModernWpfPlayground.PropertyPresenter2
{
    /// <summary>
    /// Makes an Bitmap from every Imageformat.
    /// </summary>
    public sealed class ObjectImageConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            const string dynResPrefix = "dynRes:";
            if (value is Bitmap bitmap)
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(16, 16));
            }

            if (!(value is string strValue)) return Binding.DoNothing;
            if (strValue.StartsWith(dynResPrefix, StringComparison.Ordinal))
            {
                var resource = Application.Current.TryFindResource(strValue.Replace(dynResPrefix, string.Empty , StringComparison.InvariantCulture));
                return resource is ImageSource source ? source : Binding.DoNothing;
            }

            if (strValue.StartsWith("text:", StringComparison.Ordinal))
            {
                var parts = strValue.Split(':');
                return parts.Length == 3 ? DrawText(WebUtility.HtmlDecode(parts[2]), parts[1], Brushes.Black) : Binding.DoNothing;
            }

            return GetIcon(Geometry.Parse(strValue), null);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            Binding.DoNothing;

        /// <summary>
        /// Get icon a ImageSource from geometry.
        /// </summary>
        /// <param name="geometry">geometry</param>
        /// <param name="brush">color of the icon</param>
        /// <returns></returns>
        public static ImageSource GetIcon(Geometry geometry, Brush brush)
        {
            if (brush == null)
                brush = Brushes.Black;
            var drawing = new GeometryDrawing(brush, null, geometry);
            return new DrawingImage(drawing);
        }

        /// <summary>
        /// Draw text as ImageSource from string.
        /// </summary>
        /// <param name="text">the text</param>
        /// <param name="strFontFamily">font of the string</param>
        /// <param name="brush">color of the string</param>
        /// <returns></returns>
        public static ImageSource DrawText(string text, string strFontFamily, Brush brush)
        {
            var fontFamily = new FontFamily(strFontFamily);
            var formattedText = new FormattedText(text,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface(
                    fontFamily,
                    FontStyles.Normal,
                    FontWeights.Normal,
                    FontStretches.Normal),
                64, brush);

            var geometry = formattedText.BuildGeometry(new Point(0, 0));
            return GetIcon(geometry, null);
        }
    }

    /// <summary>
    /// Invert boolean converter
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is bool b ? !b : throw new InvalidOperationException("The target must be a boolean");

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}