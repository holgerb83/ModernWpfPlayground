using System;
using System.Text.Json;

namespace ModernWpfPlayground.MvvmStuff
{
    public static class PropertyInfoExtension
    {
        public static object? Convert(this JsonElement value, Type propertyType)
        {
            if (propertyType == null) return default;

            if (propertyType == typeof(double))
            {
                return value.GetDouble();
            }

            if (propertyType == typeof(bool))
            {
                return value.GetBoolean();
            }

            if (propertyType == typeof(int))
            {
                return value.GetInt32();
            }

            if (propertyType.IsEnum)
            {
                return Enum.ToObject(propertyType, value.GetInt32());
            }

            if (propertyType == typeof(string))
            {
                return value.GetString();
            }

            return default;
        }
    }
}