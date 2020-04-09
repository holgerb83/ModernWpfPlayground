using System.ComponentModel;

namespace ModernWpfPlayground
{
    public enum ThemeMode
    {
        [Description("Light")] Light,
        [Description("Dark")] Dark,
        [Description("Use system setting")] UseSystemSetting
    }
}