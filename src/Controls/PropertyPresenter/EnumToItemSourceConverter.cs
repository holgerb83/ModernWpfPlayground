using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Controls;

/// <summary>
/// Converts enums to a List with KeyValuePairs.
/// </summary>
public class EnumToItemSourceConverter : MarkupExtension, IValueConverter
{
    private static EnumToItemSourceConverter? _converter;

    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Enum)
            return Binding.DoNothing;
        return (from object enumValue in Enum.GetValues(value.GetType()) select new KeyValuePair<string, object>(GetDescription(enumValue), enumValue)).ToList();
    }

    /// <summary>
    /// Returns the content of a description attribute of an enum.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static string GetDescription(object value)
    {
        if (value is not Enum enumValue)
            return string.Empty;
        var descriptionAttribute = enumValue.GetType().GetField(enumValue.ToString())?.GetCustomAttributes(false).OfType<DescriptionAttribute>().FirstOrDefault();

        return descriptionAttribute?.Description ?? value.ToString() ?? string.Empty;
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => _converter ??= new EnumToItemSourceConverter();
}
