using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ModernWpf;
using ModernWpfPlayground.MvvmStuff;
using Prism.Commands;
using YamlDotNet.Serialization;

namespace ModernWpfPlayground
{
    public class MainWindowViewModel : BaseViewModel
    {
        private const string AppName = "TaBEA 3.0.0";
        private string? _path;
        private string _title = AppName;
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        public MainWindowViewModel()
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
            _serializer = new SerializerBuilder().Build();
            _deserializer = new DeserializerBuilder().Build();
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

        public ThemeMode ThemeMode
        {
            get => GetProperty(ThemeMode.UseSystemSetting);
            set => SetProperty(value, SetTheme);
        }

        public int WindowWidth
        {
            get => GetProperty(1200);
            set => SetProperty(value);
        }

        public int WindowHeight
        {
            get => GetProperty(600);
            set => SetProperty(value);
        }

        private static void SetTheme(ThemeMode themeMode)
        {
            ThemeManager.Current.ApplicationTheme = themeMode switch
            {
                ThemeMode.Light => ApplicationTheme.Light,
                ThemeMode.Dark => ApplicationTheme.Dark,
                ThemeMode.UseSystemSetting => default,
                _ => ThemeManager.Current.ApplicationTheme
            };
        }

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
            var contents = _serializer.Serialize(Values);
            if (Path == null)
            {
                var saveFileDialog = new SaveFileDialog {AddExtension = true, DefaultExt = "*.yaml"};
                var result = saveFileDialog.ShowDialog(Application.Current.MainWindow?.Owner);
                if (result != true) return;
                Path = saveFileDialog.FileName;
            }

            File.WriteAllText(Path, contents);
        }

        protected override IEnumerable<(string key, object? value)> GetViewModelItems()
        {
            var openFileDialog = new OpenFileDialog {AddExtension = true, DefaultExt = "*.yaml"};
            var result = openFileDialog.ShowDialog(Application.Current.MainWindow?.Owner);
            if (result != true) yield break;

            var contents = File.ReadAllText(Path = openFileDialog.FileName);

            var obj = _deserializer.Deserialize<Dictionary<string, object>>(contents);
            foreach (var (key, value) in obj)
            {
                yield return (key, DeserializationExtension.Convert(value, ObjectAccessor[key].GetType()));
            }
        }
    }
}