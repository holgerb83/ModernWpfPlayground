using System;
using System.Globalization;

namespace ModernWpfPlayground.MvvmStuff
{
    public static class DeserializationExtension
    {
        public static object? Convert(object? value, Type propertyType)
        {
            if (value is null) return Activator.CreateInstance(propertyType);

            if (propertyType.IsEnum && value is string s)
            {
                return Enum.Parse(propertyType, s);
            }

            return System.Convert.ChangeType(value, propertyType, CultureInfo.InvariantCulture);
        }
    }
}