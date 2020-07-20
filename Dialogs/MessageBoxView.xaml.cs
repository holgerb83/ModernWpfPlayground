using System.Linq;
using System.Windows;

namespace Dialogs
{
    /// <summary>
    /// Interaktionslogik für MessageBoxView.xaml
    /// </summary>
    public partial class MessageBoxView
    {
        public MessageBoxView()
        {
            InitializeComponent();
            Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
        }
    }
}
