using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Core.UI
{
    public class LayoutMenuItem : Control
    {
        static LayoutMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutMenuItem), new FrameworkPropertyMetadata(typeof(LayoutMenuItem)));
        }

        public LayoutMenuItem()
        {
            this.SetValue(IsEnabledProperty, false);
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(LayoutMenuItem), new PropertyMetadata(null));



        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(LayoutMenuItem), new PropertyMetadata(null, CommandChanged));

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ICommand newCommand = e.NewValue as ICommand;
            if (newCommand != null)
                newCommand.CanExecuteChanged += (o, ev) => d.SetValue(IsEnabledProperty, newCommand.CanExecute(null));
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LayoutMenuItem), new PropertyMetadata(""));


    }
}
