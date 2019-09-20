using System.Windows;
using System.Windows.Controls;

namespace Core.UI
{
    public class NotificationBlock : Control
    {
        static NotificationBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NotificationBlock), new FrameworkPropertyMetadata(typeof(NotificationBlock)));
        }



        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Message.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(NotificationBlock), new PropertyMetadata(""));



    }
}
