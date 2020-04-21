using System.Windows;

namespace ModernWpfPlayground
{
    public partial class ContentDialogExample
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(ContentDialogExample), new PropertyMetadata(default(string)));

        public string Message
        {
            get => (string) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public ContentDialogExample()
        {
            InitializeComponent();
        }
    }
}
