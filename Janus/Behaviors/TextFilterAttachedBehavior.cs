

namespace Janus.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public static class TextFilterAttachedBehavior
    {
        public static readonly DependencyProperty FilterProperty = DependencyProperty.RegisterAttached(
            "Filter", typeof(string), typeof(TextFilterAttachedBehavior), new PropertyMetadata(default(string), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBox = d as TextBox;
            if (textBox != null)
            {
                WeakEventManager<TextBox, KeyEventArgs>.AddHandler(textBox, "PreviewKeyDown", TextBoxOnPreviewKeyDown);
            }
        }

        private static void TextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.A;
        }

        public static void SetFilter(UIElement element, string value)
        {
            element.SetValue(FilterProperty, value);
        }

        public static string GetFilter(UIElement element)
        {
            return (string) element.GetValue(FilterProperty);
        }
    }
}
