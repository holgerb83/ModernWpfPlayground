using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;
using ModernWpfPlayground.Annotations;

namespace ModernWpfPlayground
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _valueDict = new Dictionary<string, object>();

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

        public void SaveViewModel()
        {
            var contents = JsonSerializer.Serialize(_valueDict);
            var saveFileDialog = new SaveFileDialog();
            var result = saveFileDialog.ShowDialog(Application.Current.MainWindow?.Owner);
            if (result == true)
            {
                File.WriteAllText(saveFileDialog.FileName, contents);
            }
        }

        public void LoadViewModel(string path)
        {
            var contents = File.ReadAllText(path);
            var obj = JsonSerializer.Deserialize<Dictionary<string, object>>(contents);
            foreach (var (key, value) in obj)
            {
                _valueDict[key] = value;
                OnPropertyChanged(key);
            }
        }
    }
}