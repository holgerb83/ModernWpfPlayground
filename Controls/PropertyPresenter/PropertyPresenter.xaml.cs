using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Controls
{
    /// <inheritdoc cref="ContentControl" />
    /// <summary>
    /// Interaction logic for PropertyPresenter.xaml
    /// </summary>
    [ContentProperty(nameof(Value))]
    public sealed partial class PropertyPresenter
    {
        /// <summary>
        ///     Button alignment property.
        /// </summary>
        public static readonly DependencyProperty ButtonAlignmentProperty = DependencyProperty.Register(nameof(ButtonAlignment), typeof(Dock), typeof(PropertyPresenter), new PropertyMetadata(Dock.Right));

        /// <summary>
        ///     Content of the command property.
        /// </summary>
        public static readonly DependencyProperty CommandContentProperty = DependencyProperty.Register(nameof(CommandContent), typeof(object), typeof(PropertyPresenter), new PropertyMetadata("..."));

        /// <summary>
        ///     Command Parameter property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(PropertyPresenter), new PropertyMetadata(default(object)));

        /// <summary>
        ///     Command property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(PropertyPresenter), new PropertyMetadata(default(ICommand)));

        /// <summary>
        ///     is checked property.
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.Register(nameof(IsChecked), typeof(bool?), typeof(PropertyPresenter), new FrameworkPropertyMetadata(default(bool?), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        ///     Is readonly property
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(PropertyPresenter), new PropertyMetadata(default(bool)));

        /// <summary>
        ///     Label property
        /// </summary>
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof(Label), typeof(string), typeof(PropertyPresenter), new PropertyMetadata(default(string)));

        /// <summary>
        ///     label width property.
        /// </summary>
        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register(nameof(LabelWidth), typeof(double), typeof(PropertyPresenter), new PropertyMetadata(150.0));

        /// <summary>
        ///     Symbol Property
        /// </summary>
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(nameof(Symbol), typeof(object), typeof(PropertyPresenter), new PropertyMetadata(default(object)));

        /// <summary>
        ///     Value Property
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(object), typeof(PropertyPresenter), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        ///     Watermark Property
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(nameof(Watermark), typeof(string), typeof(PropertyPresenter), new FrameworkPropertyMetadata(default(string)));

        /// <inheritdoc />
        public PropertyPresenter()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Button alignment.
        /// </summary>
        public Dock ButtonAlignment
        {
            get => (Dock)GetValue(ButtonAlignmentProperty);
            set => SetValue(ButtonAlignmentProperty, value);
        }

        /// <summary>
        ///     Command.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        ///     Command content.
        /// </summary>
        public object CommandContent
        {
            get => GetValue(CommandContentProperty);
            set => SetValue(CommandContentProperty, value);
        }

        /// <summary>
        ///     Command parameter.
        /// </summary>
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        ///     IsChecked.
        /// </summary>
        public bool? IsChecked
        {
            get => (bool?)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        /// <summary>
        ///     IsReadOnly
        /// </summary>
        public bool IsReadOnly
        {
            get => (bool)GetValue(IsReadOnlyProperty);
            set => SetValue(IsReadOnlyProperty, value);
        }

        /// <summary>
        ///     Label.
        /// </summary>
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        /// <summary>
        ///     Label width.
        /// </summary>
        public double LabelWidth
        {
            get => (double)GetValue(LabelWidthProperty);
            set => SetValue(LabelWidthProperty, value);
        }

        /// <summary>
        ///     Symbol.
        /// </summary>
        public object Symbol
        {
            get => GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }

        /// <summary>
        ///     Value.
        /// </summary>
        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        ///     Watermark.
        /// </summary>
        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        /// <inheritdoc />
        public override string ToString() => Value != null ? base.ToString() + " " + Value : base.ToString();
    }
}