using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;

namespace ModernWpfPlayground.MvvmStuff
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        private readonly Dictionary<string, object> _valueDict = new Dictionary<string, object>();

        protected bool SetProperty<T>(T value, Action<T>? onChanged = null,
            [CallerMemberName] string? propertyName = null)
        {
            if(propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (_valueDict.TryGetValue(propertyName, out var obj) && Equals(value, obj)) return false;

            _valueDict[propertyName] = value!;
            OnPropertyChanged(propertyName);
            onChanged?.Invoke(value);
            return true;
        }

        protected T GetProperty<T>(T defaultValue = default, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            return _valueDict.TryGetValue(propertyName, out var obj) ? (T) obj : defaultValue;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        [PublicAPI]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
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

        protected void MapDictionary(IEnumerable<(string, object?)> tuples)
        {
            if (tuples == null) throw new ArgumentNullException(nameof(tuples));
            foreach (var (key, value) in tuples)
            {
                _valueDict[key] = value!;
                OnPropertyChanged(key);
            }
        }
    }
}