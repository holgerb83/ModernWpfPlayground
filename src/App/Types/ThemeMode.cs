using System.ComponentModel;
using ModernWpf;

namespace ModernWpfPlayground.Types
{
    public enum ThemeMode
    {
        [Description("Light")] Light,
        [Description("Dark")] Dark,
        [Description("Use system setting")] UseSystemSetting
    }

    public static class ThemeModeExtension
    {
        public static ApplicationTheme? ToApplicationTheme(this ThemeMode themeMode)
        {
            return themeMode switch
            {
                ThemeMode.Light => ApplicationTheme.Light,
                ThemeMode.Dark => ApplicationTheme.Dark,
                ThemeMode.UseSystemSetting => default,
                _ => throw new ArgumentOutOfRangeException(nameof(themeMode), themeMode, null)
            };
        }
    }
}