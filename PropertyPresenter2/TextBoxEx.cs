using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LsBricks.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a TextBox that can update the binding on enter.
    /// </summary>
    public class TextBoxEx : TextBox
    {
        /// <summary>
        /// Identifies the <see cref="MoveFocusOnEnter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MoveFocusOnEnterProperty =
            DependencyProperty.Register(
                "MoveFocusOnEnter", typeof(bool), typeof(TextBoxEx), new UIPropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="UpdateBindingOnEnter"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty UpdateBindingOnEnterProperty =
            DependencyProperty.Register(
                "UpdateBindingOnEnter", typeof(bool), typeof(TextBoxEx), new UIPropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="ScrollToHomeOnFocus"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScrollToHomeOnFocusProperty =
            DependencyProperty.Register("ScrollToHomeOnFocus", typeof(bool), typeof(TextBoxEx), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="SelectAllOnFocus"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectAllOnFocusProperty =
            DependencyProperty.Register("SelectAllOnFocus", typeof(bool), typeof(TextBoxEx), new PropertyMetadata(true));

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:LsBricks.Controls.TextBoxEx" /> class.
        /// </summary>
        public TextBoxEx()
        {
            GotKeyboardFocus += HandleGotKeyboardFocus;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to select all on focus.
        /// </summary>
        /// <value>
        ///   <c>true</c> if all should be selected; otherwise, <c>false</c>.
        /// </value>
        public bool SelectAllOnFocus
        {
            get => (bool)GetValue(SelectAllOnFocusProperty);
            set => SetValue(SelectAllOnFocusProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to scroll to home on focus.
        /// </summary>
        /// <value>
        /// <c>true</c> if scroll is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool ScrollToHomeOnFocus
        {
            get => (bool)GetValue(ScrollToHomeOnFocusProperty);
            set => SetValue(ScrollToHomeOnFocusProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether MoveFocusOnEnter.
        /// </summary>
        public bool MoveFocusOnEnter
        {
            get => (bool)GetValue(MoveFocusOnEnterProperty);

            set => SetValue(MoveFocusOnEnterProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether UpdateBindingOnEnter.
        /// </summary>
        public bool UpdateBindingOnEnter
        {
            get => (bool)GetValue(UpdateBindingOnEnterProperty);

            set => SetValue(UpdateBindingOnEnterProperty, value);
        }

        /// <inheritdoc />
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            switch (e.Key)
            {
                case Key.Enter:
                    if (!AcceptsReturn)
                    {
                        if (UpdateBindingOnEnter)
                        {
                            // get the binding to the Text property
                            var bindingExpression = GetBindingExpression(TextProperty);
                            // update the source (do not update the target)
                            bindingExpression?.UpdateSource();
                        }

                        if (MoveFocusOnEnter)
                        {
                            // Move focus to next element
                            // http://madprops.org/blog/enter-to-tab-in-wpf/
                            if (e.OriginalSource is UIElement uiElement)
                            {
                                var shift = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
                                uiElement.MoveFocus(new TraversalRequest(shift ? FocusNavigationDirection.Previous : FocusNavigationDirection.Next));
                            }
                        }

                        e.Handled = true;
                    }

                    break;
                case Key.Escape:
                    Undo();
                    SelectAll();
                    e.Handled = true;
                    break;
            }
        }

        /// <inheritdoc />
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);

            if (!IsKeyboardFocusWithin)
            {
                SelectAll();
                Focus();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the got keyboard focus event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs" /> instance containing the event data.</param>
        private void HandleGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (SelectAllOnFocus)
            {
                SelectAll();
            }

            if (ScrollToHomeOnFocus)
            {
                ScrollToHome();
            }

            e.Handled = true;
        }
    }
}