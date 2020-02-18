using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModernWpfPlayground
{
    public class WindowViewModel : BaseViewModel
    {
        public WindowViewModel()
        {
            ShowDialogCommand = new RelayCommand(async x => await ShowDialogAsync(x));
        }

        private async Task ShowDialogAsync(object obj)
        {
            var dialog = new ContentDialogExample();
            await dialog.ShowAsync();
        }

        public bool BooleanValue
        {
            get => GetProperty(true);
            set => SetProperty(value);
        }

        public Visibility VisibilityEnumTest
        {
            get => GetProperty<Visibility>();
            set => SetProperty(value);
        }

        public double SliderTest
        {
            get => GetProperty<double>(100);
            set => SetProperty(value);
        }

        public double ValidationTest
        {
            get => GetProperty<double>();
            set => SetProperty(value);
        }

        public ICommand ShowDialogCommand { get; }
    }
}