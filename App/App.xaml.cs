using System.Windows;
using Dialogs;
using ModernWpf;
using ModernWpfPlayground.Types;
using Prism.Ioc;
using Prism.Services.Dialogs;

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


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IDialogService, Dialogs.DialogService>();
            containerRegistry.Register<MainWindow>();
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.RegisterDialog<MessageBoxView, MessageBoxViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
    }
}
