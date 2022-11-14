using System.Windows.Media;

namespace ModernWpfPlayground.Types;

public enum AccentColors
{
    Green,
    Yellow,
    Blue,
    Purple,
    Red
}

public static class AccentColorExtension
{
    public static Color ToWindowsColor(this AccentColors accentColor)
    {
        return accentColor switch
        {
            AccentColors.Green => Color.FromRgb(0, 86, 76),
            AccentColors.Yellow => Color.FromRgb(164, 144, 0),
            AccentColors.Blue => Color.FromRgb(0, 120, 215),
            AccentColors.Purple => Color.FromRgb(104, 33, 122),
            AccentColors.Red => Color.FromRgb(183, 71, 42),
            _ => throw new ArgumentOutOfRangeException(nameof(accentColor), accentColor, null)
        };
    }
}
