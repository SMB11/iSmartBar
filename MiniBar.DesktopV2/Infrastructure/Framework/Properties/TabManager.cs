using DevExpress.Xpf.Core;
using Infrastructure.Interface;
using Infrastructure.Utility;
using System.Windows;

namespace Infrastructure
{
    public class TabManager
    {

        private static IWorkItem DraggingTabOwner;


        public static bool GetIsWorkitemTabControl(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsWorkitemTabControlProperty);
        }

        public static void SetIsWorkitemTabControl(DependencyObject obj, bool value)
        {
            obj.SetValue(IsWorkitemTabControlProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsWorkitemTabControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsWorkitemTabControlProperty =
            DependencyProperty.RegisterAttached("IsWorkitemTabControl", typeof(bool), typeof(TabManager), new PropertyMetadata(false, OnIsWorkitemTabControlChanged));

        private static void OnIsWorkitemTabControlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is DXTabControl && ((bool)e.NewValue) == true)
            {
                DXTabControl control = (DXTabControl)d;
                control.TabHiding += DXTabControl_TabHiding;
                control.SelectionChanging += DXTabControl_SelectionChanging;
                control.NewTabbedWindow += DXTabControl_NewTabbedWindow;
                control.TabStartDragging += DXTabControl_TabStartDragging;
                control.TabMoved += Control_TabMoved;
            }
        }



        private static async void DXTabControl_TabHiding(object sender, TabControlTabHidingEventArgs e)
        {
            if (e.Item is DXTabItem)
            {
                DependencyObject tabItem = e.Item as DependencyObject;
                e.Cancel = true;
                if (tabItem != null)
                    await WorkitemManager.GetOwner(tabItem).Close();
            }
        }

        private static void DXTabControl_SelectionChanging(object sender, TabControlSelectionChangingEventArgs e)
        {

            if (e.NewSelectedItem is DXTabItem)
            {
                DependencyObject tabItem = e.NewSelectedItem as DependencyObject;
                if (tabItem != null)
                {
                    IWorkItem owner = WorkitemManager.GetOwner(tabItem);
                    if (e.OldSelectedItem != null && owner != null)
                    {
                        e.Cancel = true;
                        CommandManager.ExecuteCommand(Infrastructure.Constants.CommandNames.FocusWorkitem, owner);
                    }
                    else
                    {
                        CommandManager.ExecuteCommand(Infrastructure.Constants.CommandNames.FocusWorkitem, null);
                    }
                }
            }
        }

        private static void DXTabControl_NewTabbedWindow(object sender, TabControlNewTabbedWindowEventArgs e)
        {
            IWorkItem workitem = DraggingTabOwner;
            DraggingTabOwner = null;
            if (workitem.IsModal) return;
            IWindow popup = WorkitemHelper.GetModalWindow(workitem);
            workitem.Window = popup;
            workitem.ChangeToModalState();
            e.NewWindow = (Window)workitem.Window;

            CommandManager.ExecuteCommand(Infrastructure.Constants.CommandNames.FocusWorkitem, workitem);
        }



        private static void DXTabControl_TabStartDragging(object sender, TabControlTabStartDraggingEventArgs e)
        {
            if (e.Item is DXTabItem)
            {
                DXTabItem tab = (DXTabItem)e.Item;

                IWorkItem workitem = WorkitemManager.GetOwner(tab);
                if (workitem == null) e.Cancel = true;
                DraggingTabOwner = workitem;
            }
        }

        private static void Control_TabMoved(object sender, TabControlTabMovedEventArgs e)
        {
            if (DraggingTabOwner != null && e.Item is DXTabItem)
            {

                DXTabItem tab = (DXTabItem)e.Item;

                IWorkItem workitem = WorkitemManager.GetOwner(tab);
                if (DraggingTabOwner.Equals(workitem))
                    DraggingTabOwner = null;
            }
        }
    }
}
