using DevExpress.Xpf.LayoutControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Core.UI
{
    public class InformativeTile : Tile
    {
        static InformativeTile()
        {
            CommandProperty.OverrideMetadata(typeof(InformativeTile), new PropertyMetadata(CommandChanged));
        }

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ICommand newCommand = e.NewValue as ICommand;
            d.SetValue(IsEnabledProperty, newCommand.CanExecute(d.GetValue(CommandParameterProperty)));
            if (newCommand != null)
                newCommand.CanExecuteChanged += (o, ev) => d.SetValue(IsEnabledProperty, newCommand.CanExecute(d.GetValue(CommandParameterProperty)));
        }

        public string InformativeText
        {
            get { return (string)GetValue(InformativeTextProperty); }
            set { SetValue(InformativeTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InformativeText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InformativeTextProperty =
            DependencyProperty.Register("InformativeText", typeof(string), typeof(InformativeTile), new UIPropertyMetadata(""));
        

    }
}
