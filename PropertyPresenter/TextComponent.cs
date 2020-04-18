using System.Windows;

namespace ModernWpfPlayground.PropertyPresenter2
{
    /// <summary>
    /// A component of the symbol
    /// </summary>
    public readonly struct TextComponent
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        /// <param name="style"></param>
        public TextComponent(string text, BaselineAlignment style = BaselineAlignment.Baseline)
        {
            Text = text;
            Style = style;
        }

        /// <summary>
        /// Text of the symbol component
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// Style of the symbol component
        /// </summary>
        public readonly BaselineAlignment Style;
    }
}
