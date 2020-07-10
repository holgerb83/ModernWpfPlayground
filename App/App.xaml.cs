using System.Windows;
using System.Windows.Media;
using ModernWpf;
using Prism.Ioc;

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
            ThemeManager.Current.AccentColor = Color.FromArgb(255, 0, 86, 76);
            base.OnStartup(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindow>();
            containerRegistry.Register<MainWindowViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
