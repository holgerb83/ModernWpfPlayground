using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ModernWpfPlayground.MvvmStuff;

namespace ModernWpfPlayground
{
    public class WindowViewModel : BaseViewModel
    {
        public WindowViewModel()
        {
            ShowDialogCommand = new RelayCommand(async x => await ShowDialogAsync());
            CloseCommand = new RelayCommand(x => Application.Current.Shutdown());
            OpenViewModelCommand = new RelayCommand(x => LoadViewModel());
            SaveViewModelCommand = new RelayCommand(x => SaveViewModel());
            ResetViewModelCommand = new RelayCommand(x => ResetViewModel());
        }

        private async Task ShowDialogAsync()
        {
            var dialog = new ContentDialogExample {Message = WelcomeMessage};
            var result = await dialog.ShowAsync();
            WelcomeMessage = result.ToString();
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

        public RelayCommand ShowDialogCommand { get; }

        public string WelcomeMessage
        {
            get => GetProperty("Shadow of the empire");
            set => SetProperty(value);
        }

        public ICommand CloseCommand { get; }

        public ICommand OpenViewModelCommand { get; }

        public ICommand SaveViewModelCommand { get; }

        public ICommand ResetViewModelCommand { get; }
    }
}