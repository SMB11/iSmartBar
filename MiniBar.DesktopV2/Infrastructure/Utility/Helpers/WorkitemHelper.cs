using Infrastructure.Interface;
using Infrastructure.Modularity;
using Infrastructure.Utility;
using Prism.Regions;
using System.Windows;

namespace Infrastructure.Utility
{
    internal class WorkitemHelper
    {
        internal static IWindow GetModalWindow(IWorkItem workitem, IModalOption metadata = null)
        {
            if(metadata == null)
            {
                metadata = workitem.Configuration.GetOption<IModalOption>();
            }
            ModalRegionPopup popup = new ModalRegionPopup();
            global::Prism.Regions.RegionManager.SetRegionManager(popup, RegionManager.GetRegionManager(Application.Current.MainWindow));
            global::Prism.Regions.RegionManager.UpdateRegions();
            popup.WindowStartupLocation = metadata.WindowStartupLocation;
            if (!metadata.Size.IsEmpty)
            {
                popup.Width = metadata.Size.Width;
                popup.Height = metadata.Size.Height;
            }
            popup.ShowIcon = metadata.ShowIcon;
            popup.ResizeMode = metadata.ResizeMode;
            popup.Title = workitem.WorkItemName;
            popup.Closing += (o, args) => Popup_Closing(workitem, args);

            return popup;
        }

        private static async void Popup_Closing(IWorkItem workitem, System.ComponentModel.CancelEventArgs e)
        {
            if (!(await workitem.Close()))
            {
                e.Cancel = true;
                return;
            }

            RegionManager.SetRegionManager((DependencyObject)workitem.Window, null);
            RegionManager.UpdateRegions();
        }
    }
}
