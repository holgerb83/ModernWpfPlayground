using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using ModernWpfPlayground.MvvmStuff;
using ModernWpfPlayground.Types;
using Prism.Commands;
using YamlDotNet.Serialization;
using static ModernWpf.ThemeManager;

namespace ModernWpfPlayground
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MainWindowViewModel : BaseViewModel
    {
        private const string AppName = "TaBEA 3.0.0";
        private string? _path;
        private string _title = AppName;
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        public MainWindowViewModel()
        {
            ShowDialogCommand = new DelegateCommand(ShowDialog);
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

        private string? Path
        {
            get => _path;
            set => SetProperty(ref _path, value,
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
            get => GetProperty(Visibility.Visible);
            set => SetProperty(value);
        }

        public double SliderTest
        {
            get => GetProperty(100D);
            set => SetProperty(value);
        }

        public double ValidationTest
        {
            get => GetProperty(0D);
            set => SetProperty(value);
        }

        public ICommand ShowDialogCommand { get; }

        public string? WelcomeMessage
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

        public AccentColors AccentColors
        {
            get => GetProperty(AccentColors.Green);
            set => SetProperty(value, SetAccentColor);
        }

        private static void SetAccentColor(AccentColors accentColors) => Current.AccentColor = accentColors.ToWindowsColor();

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

        public bool IsPaneOpen
        {
            get => GetProperty(true);
            set => SetProperty(value);
        }

        private static void SetTheme(ThemeMode themeMode) => Current.ApplicationTheme = themeMode.ToApplicationTheme();

        private void ShowDialog()
        {
            var dialog = new ContentDialogExample {Message = WelcomeMessage};
            dialog.ShowAsync().Await(completedCallback: x => WelcomeMessage = x.ToString());
        }

        private void BooleanValue_OnChanged(bool obj)
        {
            VisibilityEnumTest = obj ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SaveViewModel()
        {
            var contents = _serializer.Serialize(Values);
            if (Path is null)
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