using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Janus.Behaviors
{
    public class TextInputFilterBehavior : Behavior<TextBox>
    {
        static readonly List<Key> ValidKeys = new List<Key>() {Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9 };

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewKeyDown += OnPreviewKeyDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewKeyDown -= OnPreviewKeyDown;
        }

        private static void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !ValidKeys.Contains(e.Key);
        }
    }
}