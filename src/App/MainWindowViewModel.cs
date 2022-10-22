using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernWpfPlayground.Types;
using static ModernWpf.ThemeManager;

namespace ModernWpfPlayground
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public partial class MainWindowViewModel : ObservableObject
    {
        private const string AppName = "TaBEA 3.0.0";

        [ObservableProperty] private string? _path;
        [ObservableProperty] private string _title = AppName;
        [ObservableProperty] private bool _booleanValue = true;
        [ObservableProperty] private Visibility _visibilityEnumTest = Visibility.Visible;
        [ObservableProperty] private double _sliderTest = 100;
        [ObservableProperty] private double _validationTest;
        [ObservableProperty] private string? _welcomeMessage = "Shadow of the empire";
        [ObservableProperty] private ThemeMode _themeMode = ThemeMode.UseSystemSetting;
        [ObservableProperty] private AccentColors _accentColors = AccentColors.Green;
        [ObservableProperty] private int _windowWidth = 1200;
        [ObservableProperty] private int _windowHeight = 600;
        [ObservableProperty] private bool _isPaneOpen = true;

        partial void OnBooleanValueChanged(bool value)
        {
            VisibilityEnumTest = value ? Visibility.Visible : Visibility.Collapsed;
        }

        partial void OnPathChanged(string? value)
        {
            Title = value != null ? $"{System.IO.Path.GetFileName(value)} - {AppName}" : AppName;
        }


        [RelayCommand]
        private void ShowNotification()
        {
        }

        [RelayCommand]
        private void Close()
        {
            Application.Current.MainWindow?.Close();
        }

        partial void OnThemeModeChanged(ThemeMode value)
        {
            Current.ApplicationTheme = value.ToApplicationTheme();
        }

        partial void OnAccentColorsChanged(AccentColors value)
        {
            Current.AccentColor = value.ToWindowsColor();
        }

        [RelayCommand]
        private async void ShowDialog()
        {
            var dialog = new ContentDialogExample { Message = WelcomeMessage };
            var result = await dialog.ShowAsync();
            WelcomeMessage = result.ToString();
        }

        [RelayCommand]
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