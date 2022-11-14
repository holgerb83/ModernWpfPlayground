using System.Windows;
using ModernWpf;
using ModernWpfPlayground.Types;

namespace ModernWpfPlayground
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemeManager.Current.AccentColor = AccentColors.Green.ToWindowsColor();
        }
    }
}
