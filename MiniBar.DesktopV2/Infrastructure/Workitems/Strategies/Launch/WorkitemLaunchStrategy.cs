using Infrastructure.Extensions;
using Infrastructure.Util;
using Infrastructure.Interface;
using Infrastructure.Prism.Regions;
using Infrastructure.Workitems.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Infrastructure.Workitems.Strategies.Launch
{
    internal abstract class WorkitemLaunchStrategy
    {
        protected CurrentContextService CurrentContextService { get; private set; }
        protected IWorkItem Workitem { get; private set; }
        protected IWorkItem Parent { get; private set; }
        protected object Data { get; private set; }

        internal WorkitemLaunchStrategy(CurrentContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null) {
            CurrentContextService = currentContextService;
            Workitem = workItem;
            Parent = parent;
            Data = data;
        }

        public static WorkitemLaunchStrategy GetLaunchStrategy(CurrentContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null)
        {
            if(parent != null)
                return new ChildWorkitemLaunchStrategy(currentContextService, workItem, parent, data);
            else
                return new RootWorkitemLaunchStrategy(currentContextService, workItem, parent, data);
        }

        protected abstract void Execute();

        public void Launch()
        {
            try
            {
                Type type = Workitem.GetType();
                SingleInstanceWorkitemAttribute attribute = type.GetCustomAttributes(typeof(SingleInstanceWorkitemAttribute), false).FirstOrDefault() as SingleInstanceWorkitemAttribute;
                if (attribute != null)
                {
                    IWorkItem exists = CurrentContextService.Collection.Where(w => w.GetType().IsSubclassOf(type)).FirstOrDefault();
                    if (exists != null)
                    {
                        Application.Current.Dispatcher.InvokeIfNeeded(() => CurrentContextService.FocusWorkitem(exists));
                        return;
                    }
                }

                if (Workitem is ISupportsInitialization)
                {
                    var initable = Workitem as ISupportsInitialization;
                    try
                    {
                        initable.Initialize(Data);
                    }
                    catch
                    {
                        UIHelper.Error("Failed to Initialize Workitem");
                        return;
                    }
                }

                if (Workitem is IModalWorkitem)
                {

                    IModalWorkitem workitem = Workitem as IModalWorkitem;
                    ModalRegionPopup popup = new ModalRegionPopup();
                    popup.WindowStartupLocation = workitem.WindowStartupLocation;
                    if (!workitem.Size.IsEmpty)
                    {
                        popup.Width = workitem.Size.Width;
                        popup.Height = workitem.Size.Height;
                    }
                    popup.ShowIcon = workitem.ShowIcon;
                    popup.ResizeMode = workitem.ResizeMode;
                    popup.Title = workitem.WorkItemName;
                    popup.Closing += Popup_Closing;
                    workitem.Popup = popup;
                }

                Execute();

                if (Workitem is IModalWorkitem)
                {

                    IModalWorkitem workitem = Workitem as IModalWorkitem;
                    workitem.Window.ShowDialog();
                }
            }
            catch (Exception e)
            {
                UIHelper.Error("Failed to open workitem");
            }
        }


        private void Popup_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !Workitem.Close();
        }
    }
}
