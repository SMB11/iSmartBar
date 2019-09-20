using DevExpress.Xpf.Core;
using Infrastructure;
using Infrastructure.Framework;
using Infrastructure.Modularity;
using Infrastructure.Security;
using System.ComponentModel;

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
            AppSecurityContext.AppPrincipalChanged += (o, e) => HandleAuthenticationStateChanged();
            var names = new DynamicRegionNames();
            RegionNameManager.SetDynamicRegionNames(this, names);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = !CommonServiceLocator.ServiceLocator.Current.GetInstance<IUIManager>().AskForConfirmation("Are you sure you want to exit? Unsaved changes will be lost.");
        }

        private void HandleAuthenticationStateChanged()
        {
            if (AppSecurityContext.CurrentPrincipal.Identity is AnonymousIdentity)
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

    }
}
