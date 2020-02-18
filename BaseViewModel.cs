using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;

namespace ModernWpfPlayground
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel()
        {
            _properties = GetType().GetProperties();
        }

        private readonly Dictionary<string, object> _valueDict = new Dictionary<string, object>();
        private readonly PropertyInfo[] _properties;

        protected bool SetProperty<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (_valueDict.TryGetValue(propertyName, out var obj) && Equals(value, obj)) return false;

            _valueDict[propertyName] = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected T GetProperty<T>(T defaultValue = default, [CallerMemberName] string propertyName = null)
        {
            return _valueDict.TryGetValue(propertyName, out var obj) ? (T) obj : defaultValue;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        [PublicAPI]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SaveViewModel()
        {
            var contents = JsonSerializer.Serialize(_valueDict);
            var saveFileDialog = new SaveFileDialog {AddExtension = true, DefaultExt = "*.json"};
            var result = saveFileDialog.ShowDialog(Application.Current.MainWindow?.Owner);
            if (result == true)
            {
                File.WriteAllText(saveFileDialog.FileName, contents);
            }
        }

        protected void ResetViewModel()
        {
            foreach (var key in _valueDict.Keys)
            {
                _valueDict.Remove(key);
                OnPropertyChanged(key);
            }
        }

        protected void LoadViewModel()
        {
            var openFileDialog = new OpenFileDialog {AddExtension = true, DefaultExt = "*.json"};
            var result = openFileDialog.ShowDialog(Application.Current.MainWindow?.Owner);
            if (result != true) return;
            var contents = File.ReadAllText(openFileDialog.FileName);
            var obj = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(contents);
            foreach (var (key, value) in obj)
            {
                _valueDict[key] = CastToType(key, value);
                OnPropertyChanged(key);
            }
        }

        private object CastToType(string key, JsonElement value)
        {

            var property = Array.Find(_properties, x => x.Name == key);
            if (property.PropertyType == typeof(double))
            {
                return value.GetDouble();
            }

            if (property.PropertyType == typeof(bool))
            {
                return value.GetBoolean();
            }

            if (property.PropertyType == typeof(int))
            {
                return value.GetInt32();
            }

            if (property.PropertyType.IsEnum)
            {
                return Enum.ToObject(property.PropertyType, value.GetInt32());
            }

            if (property.PropertyType == typeof(string))
            {
                return value.GetString();
            }

            return default;
        }
    }
}