using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModernWpfPlayground.MvvmStuff
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _values = new Dictionary<string, object>();

        protected IReadOnlyDictionary<string, object> Values => _values;

        protected bool SetProperty<T>(T value, Action<T>? onChanged = null,
            [CallerMemberName] string? propertyName = null)
        {
            if(propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (_values.TryGetValue(propertyName, out var obj) && Equals(value, obj)) return false;

            _values[propertyName] = value!;
            OnPropertyChanged(propertyName);
            onChanged?.Invoke(value);
            return true;
        }

        protected T GetProperty<T>(T defaultValue = default, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            return Values.TryGetValue(propertyName, out var obj) ? (T) obj : defaultValue;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ResetViewModel()
        {
            foreach (var key in Values.Keys)
            {
                _values.Remove(key);
                OnPropertyChanged(key);
            }
        }

        protected abstract IEnumerable<(string key, object? value)> GetViewModelItems();

        protected void LoadViewModel()
        {
            foreach (var (key, value) in GetViewModelItems())
            {
                _values[key] = value!;
                OnPropertyChanged(key);
            }
        }
    }
}