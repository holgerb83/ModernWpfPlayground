using System;
using System.Windows;
using System.Windows.Controls;

namespace ModernWpfPlayground.PropertyPresenter2
{
    /// <summary>
    /// Selects the right template on base of value-type.
    /// </summary>
    public class PropertyDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Default data template. (currently Textbox)
        /// </summary>
        public DataTemplate DefaultDataTemplate { private get; set; }

        /// <summary>
        /// Data template for boolean. (currently Checkbox)
        /// </summary>
        public DataTemplate BooleanDataTemplate { private get; set; }

        /// <summary>
        /// Data template for enums. (currently Combobox)
        /// </summary>
        public DataTemplate EnumComboBoxDataTemplate { private get; set; }

        /// <inheritdoc />
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return item switch
            {
                bool _ => BooleanDataTemplate,
                Enum _ => EnumComboBoxDataTemplate,
                UIElement _ => null,
                _ => DefaultDataTemplate
            };
        }
    }
}