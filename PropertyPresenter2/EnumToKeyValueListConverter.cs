using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace LsBricks.Controls
{
    /// <summary>
    /// Converts enums to a List with KeyValuePairs.
    /// </summary>
    public class EnumToKeyValueListConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Enum)) return Binding.DoNothing;
            return (from object enumValue in Enum.GetValues(value.GetType())
                    select new KeyValuePair<string, object>(GetDescription(enumValue), enumValue)).ToList();
        }

        /// <summary>
        /// Returns the content of a description attribute of an enum.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        private static string GetDescription(object enumValue)
        {
            var descriptionAttribute = enumValue.GetType()
              .GetField(enumValue.ToString()).GetCustomAttributes(false)
              .OfType<DescriptionAttribute>().FirstOrDefault();

            return descriptionAttribute?.Description ?? enumValue.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}