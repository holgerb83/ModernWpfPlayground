using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

    }
}