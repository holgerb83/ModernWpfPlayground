using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace Controls;

/// <summary>
///     Magically converts a text to 
/// </summary>
[ValueConversion(typeof(string), typeof(FrameworkElement))]
public class MagicSymbolConverter : IValueConverter
{
    private const string NoParseKeyword = "noParse:";
    private const string PathKeyword = "path:";
    private const string DynResKeyword = "dynRes:";

    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ConvertToFrameworkElement(value);
    }

    /// <summary>
    /// Convert string to Framework element.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static object ConvertToFrameworkElement(object value)
    {
        var data = value as string;
        if (string.IsNullOrWhiteSpace(data)) return value; //maybe not a string. eventually something else.

        if (data.StartsWith(NoParseKeyword, StringComparison.Ordinal)) return data[NoParseKeyword.Length..];

        if (data.StartsWith(PathKeyword, StringComparison.Ordinal))
        {
            var path = data[PathKeyword.Length..];
            var icon = ObjectImageConverter.GetIcon(Geometry.Parse(path), Brushes.Black);
            return new Image
            {
                Source = icon,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 16
            };
        }

        if (data.StartsWith(DynResKeyword, StringComparison.Ordinal))
        {
            var resourceKey = data[DynResKeyword.Length..];
            //get icon from resource dictionary
            return new Image
            {
                Source = Application.Current.FindResource(resourceKey) as ImageSource,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 16
            };
        }

        var textComponents = ParseText(data);
        return CreateTextBlock(textComponents);
    }

    private static TextBlock CreateTextBlock(IEnumerable<TextComponent> textComponents)
    {
        //create text block
        var block = new TextBlock();
        foreach (var tc in textComponents)
        {
            var run = new Run(tc.Text) {FontFamily = new FontFamily("Palatino Linotype"), FontSize = 16};
            switch (tc.Style)
            {
                case BaselineAlignment.Subscript:
                    run.BaselineAlignment = BaselineAlignment.Subscript;
                    run.FontSize = 12;
                    break;
                case BaselineAlignment.Superscript:
                    run.BaselineAlignment = BaselineAlignment.Superscript;
                    run.FontSize = 12;
                    break;
            }

            block.Inlines.Add(run);
        }

        block.HorizontalAlignment = HorizontalAlignment.Right;
        block.VerticalAlignment = VerticalAlignment.Center;
        return block;
    }

    private static IEnumerable<TextComponent> ParseText(string data)
    {
        //parse text
        var textComponents = new List<TextComponent>();
        var alignment = BaselineAlignment.Baseline;
        var snippet = new StringBuilder();
        foreach (var c in data)
        {
            switch (c)
            {
                case '~':
                    if (snippet.Length > 0)
                    {
                        var comp = new TextComponent(snippet.ToString(), alignment);
                        textComponents.Add(comp);
                        snippet.Clear();
                    }

                    alignment = alignment == BaselineAlignment.Subscript
                        ? BaselineAlignment.Baseline
                        : BaselineAlignment.Subscript;
                    break;
                case '^':
                    if (snippet.Length > 0)
                    {
                        var comp = new TextComponent(snippet.ToString(), alignment);
                        textComponents.Add(comp);
                        snippet.Clear();
                    }

                    alignment = alignment == BaselineAlignment.Superscript
                        ? BaselineAlignment.Baseline
                        : BaselineAlignment.Superscript;
                    break;
                default:
                    snippet.Append(c);
                    break;
            }
        }

        if (snippet.Length > 0)
        {
            var comp = new TextComponent(snippet.ToString(), alignment);
            textComponents.Add(comp);
            snippet.Clear();
        }

        return textComponents;
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => Binding.DoNothing;
}