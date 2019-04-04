using System.ComponentModel;
using System.Windows;
using DevExpress.Xpf.Core;
using Infrastructure.Security;
using Infrastructure.Security.Entities;
using Prism.Events;

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
            //IWorkspaceManager workspaceManager = WorkspaceManager.GetWorkspaceManager(dockLayoutManager);
            //workspaceManager.LoadWorkspace("SavedWorkspace", "layout.xml");

            //workspaceManager.ApplyWorkspace("SavedWorkspace");
            AppSecurityContext.AppPrincipalChanged += (o,e) => HandleAuthenticationStateChanged();
            loginBarItem.PerformClick();
            exit.ItemClick += Exit_ItemClick;
        }

        private void Exit_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            MessageBoxResult res = DXMessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo);
            if(res == MessageBoxResult.Yes)
                this.Close();
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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //IWorkspaceManager workspaceManager = WorkspaceManager.GetWorkspaceManager(dockLayoutManager);
            //workspaceManager.CaptureWorkspace("SavedWorkspace");
            //workspaceManager.SaveWorkspace("SavedWorkspace", "layout.xml");
            

        }

        
    }
}
