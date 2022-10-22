using System.Windows;
using System.Windows.Controls.Primitives;

namespace ModernWpfPlayground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ZoomSlider.Value = 100;
        }
    }
}
