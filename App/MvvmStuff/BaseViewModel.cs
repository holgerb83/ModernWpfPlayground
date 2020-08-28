using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FastMember;
using Prism.Mvvm;

namespace ModernWpfPlayground.MvvmStuff
{
    public abstract class BaseViewModel : BindableBase
    {
        protected BaseViewModel()
        {
            ObjectAccessor = ObjectAccessor.Create(this);
        }

        private readonly Dictionary<string, object?> _values = new Dictionary<string, object?>();
        protected readonly ObjectAccessor ObjectAccessor;
        private readonly Dictionary<string, object?> _defaultValues = new Dictionary<string, object?>();

        protected IReadOnlyDictionary<string, object?> Values => _values;

        protected bool SetProperty<T>(T value, Action<T>? onChanged = null,
            [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (_values.TryGetValue(propertyName, out var obj) && Equals(value, obj)) return false;

            _values[propertyName] = value;
            RaisePropertyChanged(propertyName);
            onChanged?.Invoke(value);
            return true;
        }

        protected T GetProperty<T>(T defaultValue = default, [CallerMemberName] string? propertyName = null)
        {
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));
            if (Values.TryGetValue(propertyName, out var obj))
            {
                return (T) obj!;
            }

            _defaultValues[propertyName] =defaultValue;
            return defaultValue;
        }

        protected void ResetViewModel()
        {
            foreach (var (key, value) in _values.ToArray())
            {
                if (_defaultValues.TryGetValue(key, out var defaultValue) && Equals(value, defaultValue)) continue;
                ObjectAccessor[key] = defaultValue;
            }
        }

        protected abstract IEnumerable<(string key, object? value)> GetViewModelItems();

        protected void LoadViewModel()
        {
            ResetViewModel();
            foreach (var (key, value) in GetViewModelItems())
            {
                ObjectAccessor[key] = value;
            }
        }
    }
}