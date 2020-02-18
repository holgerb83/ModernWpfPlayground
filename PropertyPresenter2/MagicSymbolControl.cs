using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ModernWpfPlayground.PropertyPresenter2
{
    /// <summary>
    /// Interaction logic for <see cref="MagicSymbolControl"/>
    /// </summary>
    [ContentProperty(nameof(Symbol))]
    public class MagicSymbolControl : ContentControl
    {
        /// <summary>
        /// Dependency property for <see cref="Symbol"/> property
        /// </summary>
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(nameof(Symbol), typeof(object), typeof(MagicSymbolControl), new PropertyMetadata(default, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MagicSymbolControl magic)
            {
                magic.Content = MagicSymbolConverter.ConvertToFrameworkElement(e.NewValue);
            }
        }

        /// <summary>
        /// Symbol to show
        /// </summary>
        public object Symbol
        {
            get => GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }

        /// <summary>
        /// Creates a new instance of <see cref="MagicSymbolControl"/>
        /// </summary>
        public MagicSymbolControl()
        {
            Focusable = false;
        }
    }
}