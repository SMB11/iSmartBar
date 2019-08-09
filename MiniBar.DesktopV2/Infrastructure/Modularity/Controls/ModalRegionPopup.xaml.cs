using DevExpress.Xpf.Core;
using Infrastructure.Interface;
using Infrastructure.Framework;
using Infrastructure.Utility;

namespace Infrastructure.Modularity
{
    /// <summary>
    /// Interaction logic for ModalRegionPopup.xaml
    /// </summary>
    internal partial class ModalRegionPopup : ThemedWindow, IWindow
    {
        ITaskManager TaskManager => CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();

        public ModalRegionPopup()
        {
            InitializeComponent();
            var names = new DynamicRegionNames();
            RegionNameManager.SetDynamicRegionNames(this, names);
            
        }

        #region IWindow Implementation

        void IWindow.Focus()
        {

            TaskManager.RunUIThread(() =>
            {
                UIHelper.TryFocusWindow(this);
            });
        }

        void IWindow.Unfocus()
        {

            TaskManager.RunUIThread(() =>
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            });
        }
        #endregion
    }
}
