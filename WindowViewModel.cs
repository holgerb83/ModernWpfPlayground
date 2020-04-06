using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ModernWpfPlayground.MvvmStuff;
using Prism.Commands;

namespace ModernWpfPlayground
{
    public class WindowViewModel : BaseViewModel
    {
        private const string AppName = "TaBEA 3.0.0";
        private readonly PropertyInfo[] _properties;
        private string? _path;
        private string _title = AppName;

        public WindowViewModel()
        {
            ShowDialogCommand = new DelegateCommand(async () => await ShowDialogAsync().ConfigureAwait(false));
            CloseCommand = new DelegateCommand(() => Application.Current.Shutdown());
            OpenViewModelCommand = new DelegateCommand(LoadViewModel);
            SaveViewModelCommand = new DelegateCommand(SaveViewModel);
            ResetViewModelCommand = new DelegateCommand(() =>
            {
                ResetViewModel();
                Path = null;
            });
            _properties = GetType().GetProperties();
        }

        public string? Path
        {
            get => _path;
            private set => SetProperty(ref _path, value,
                    () => Title = value != null ? $"{System.IO.Path.GetFileName(value)} - {AppName}" : AppName);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool BooleanValue
        {
            get => GetProperty(true);
            set => SetProperty(value, BooleanValue_OnChanged);
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

        public string WelcomeMessage
        {
            get => GetProperty("Shadow of the empire");
            set => SetProperty(value);
        }

        public ICommand CloseCommand { get; }

        public ICommand OpenViewModelCommand { get; }

        public ICommand SaveViewModelCommand { get; }

        public ICommand ResetViewModelCommand { get; }

        private async Task ShowDialogAsync()
        {
            var dialog = new ContentDialogExample {Message = WelcomeMessage};
            var result = await dialog.ShowAsync().ConfigureAwait(false);
            WelcomeMessage = result.ToString();
        }

        private void BooleanValue_OnChanged(bool obj)
        {
            VisibilityEnumTest = obj ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SaveViewModel()
        {
            var contents = JsonSerializer.Serialize(Values);
            if (Path == null)
            {
                var saveFileDialog = new SaveFileDialog {AddExtension = true, DefaultExt = "*.json"};
                var result = saveFileDialog.ShowDialog(Application.Current.MainWindow?.Owner);
                if (result != true) return;
                Path = saveFileDialog.FileName;
            }

            File.WriteAllText(Path, contents);
        }

        protected override IEnumerable<(string key, object? value)> GetViewModelItems()
        {
            var openFileDialog = new OpenFileDialog {AddExtension = true, DefaultExt = "*.json"};
            var result = openFileDialog.ShowDialog(Application.Current.MainWindow?.Owner);
            if (result != true) yield break;

            var contents = File.ReadAllText(Path = openFileDialog.FileName);
            var obj = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(contents);
            foreach (var (key, value) in obj)
            {
                yield return (key, _properties.Find(key).Convert(value));
            }
        }
    }
}