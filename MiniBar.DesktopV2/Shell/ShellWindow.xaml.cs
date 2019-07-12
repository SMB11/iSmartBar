using System;
using System.ComponentModel;
using System.Windows;
using DevExpress.Xpf.Core;
using Infrastructure.DX;
using Infrastructure;
using Infrastructure.Interface;
using Infrastructure.Security;
using Infrastructure.Security.Entities;
using Prism.Events;
using Infrastructure.Util;

namespace Shell
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : ThemedWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
            AppSecurityContext.AppPrincipalChanged += (o,e) => HandleAuthenticationStateChanged();
            loginBarItem.PerformClick();
            exit.ItemClick += Exit_ItemClick;
        }
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
        }

        private void Exit_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = !UIHelper.AskForConfirmation("Are you sure you want to exit? Unsaved changes will be lost.");
        }

        private void HandleAuthenticationStateChanged()
        {
            if(AppSecurityContext.CurrentPrincipal.Identity is AnonymousIdentity)
            {
                loginBarItem.IsVisible = true;
                logoutBarItem.IsVisible = false;
            }
            else
            {
                loginBarItem.IsVisible = false;
                logoutBarItem.IsVisible = true;
            }
        }

        private void DXTabControl_TabHiding(object sender, TabControlTabHidingEventArgs e)
        {
            DependencyObject tabItem = e.Item as DependencyObject;
            e.Cancel = true;
            if (tabItem != null)
                WorkitemManager.GetOwner(tabItem).Close();
        }

        private void DXTabControl_SelectionChanging(object sender, TabControlSelectionChangingEventArgs e)
        {

            DependencyObject tabItem = e.NewSelectedItem as DependencyObject;
            if (tabItem != null)
            {
                IWorkItem owner = WorkitemManager.GetOwner(tabItem);
                if (e.OldSelectedItem != null && owner != null)
                    e.Cancel = true;
                CommandManager.ExecuteCommand(Infrastructure.Constants.CommandNames.FocusWorkitem, owner);
            }
        }
    }
}
