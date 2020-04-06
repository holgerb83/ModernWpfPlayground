using System;
using System.Reflection;
using System.Text.Json;

namespace ModernWpfPlayground.MvvmStuff
{
    public static class PropertyInfoExtension
    {
        public static PropertyInfo? Find(this PropertyInfo[] infos, string key) => Array.Find(infos, x => x.Name == key);

        public static object? Convert(this PropertyInfo? propertyInfo, JsonElement value)
        {
            if (propertyInfo == null) return default;

            if (propertyInfo.PropertyType == typeof(double))
            {
                return value.GetDouble();
            }

            if (propertyInfo.PropertyType == typeof(bool))
            {
                return value.GetBoolean();
            }

            if (propertyInfo.PropertyType == typeof(int))
            {
                return value.GetInt32();
            }

            if (propertyInfo.PropertyType.IsEnum)
            {
                return Enum.ToObject(propertyInfo.PropertyType, value.GetInt32());
            }

            if (propertyInfo.PropertyType == typeof(string))
            {
                return value.GetString();
            }

            return default;
        }
    }
}