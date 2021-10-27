using ModernWpfPlayground.Types;
using static ModernWpf.ThemeManager;

namespace ModernWpfPlayground
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [ViewModel]
    public partial class MainWindowViewModel
    {
        private const string AppName = "TaBEA 3.0.0";

        [Property, PropertyCallMethod(nameof(SetTitle))]
        private string? _path;

        [Property] private string _title = AppName;

        [Property, PropertyCallMethod(nameof(BooleanValue_OnChanged))]
        private bool _booleanValue = true;

        [Property] private Visibility _visibilityEnumTest = Visibility.Visible;
        [Property] private double _sliderTest = 100;
        [Property] private double _validationTest;
        [Property] private string? _welcomeMessage = "Shadow of the empire";

        [Property, PropertyCallMethod(nameof(SetTheme))]
        private ThemeMode _themeMode = ThemeMode.UseSystemSetting;

        [Property, PropertyCallMethod(nameof(SetAccentColor))]
        private AccentColors _accentColors = AccentColors.Green;

        [Property] private int _windowWidth = 1200;
        [Property] private int _windowHeight = 600;
        [Property] private bool _isPaneOpen = true;


        [Command]
        private void ShowNotification()
        {
        }

        [Command]
        private void Close()
        {
            Application.Current.MainWindow?.Close();
        }

        private void SetTitle()
        {
            Title = Path != null ? $"{System.IO.Path.GetFileName(Path)} - {AppName}" : AppName;
        }

        private void SetAccentColor() => Current.AccentColor = AccentColors.ToWindowsColor();


        private void SetTheme() => Current.ApplicationTheme = ThemeMode.ToApplicationTheme();

        [Command]
        private async void ShowDialog()
        {
            var dialog = new ContentDialogExample { Message = WelcomeMessage };
            var result = await dialog.ShowAsync();
            WelcomeMessage = result.ToString();
        }

        private void BooleanValue_OnChanged()
        {
            VisibilityEnumTest = BooleanValue ? Visibility.Visible : Visibility.Collapsed;
        }

        [Command]
        private void SaveViewModel()
        {
            // var contents = _serializer.Serialize(Values);
            // if (Path is null)
            // {
            //     var saveFileDialog = new SaveFileDialog {AddExtension = true, DefaultExt = "*.yaml"};
            //     var result = saveFileDialog.ShowDialog(Application.Current.MainWindow?.Owner);
            //     if (result != true) return;
            //     Path = saveFileDialog.FileName;
            // }
            //
            // File.WriteAllText(Path, contents);
        }
    }
}