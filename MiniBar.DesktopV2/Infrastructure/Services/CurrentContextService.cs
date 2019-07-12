using DevExpress.Xpf.Core;
using Infrastructure.DX;
using Infrastructure.Configuration;
using Infrastructure.Constants;
using Infrastructure.Extensions;
using Infrastructure.Interface;
using Infrastructure.Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Infrastructure.Util;
using Infrastructure.Prism.Regions;
using Infrastructure.Workitems.Strategies.Launch;
using Infrastructure.Workitems.Strategies.Close;
using Infrastructure.Workitems.Strategies.Focus;
using System.Collections.Specialized;

namespace Infrastructure.Workitems
{
    public class CurrentContextService : ICurrentContextService
    {
        private IContainerExtension Container { get; set; }

        internal IRegionManager IRegionManager { get; set; }

        public CurrentContextService(IRegionManager regionManager, IContainerExtension container)
        {
            Container = container;
            IRegionManager = regionManager;
            Collection = new WorkitemCollection();
            ShellTitle = AppConfigurationManager.Title;
            CommandManager.RegisterCommand(CommandNames.FocusWorkitem, new DelegateCommand<IWorkItem>(FocusWorkitem));
            CommandManager.RegisterCommand(CommandNames.CloseAllTabs, new DelegateCommand(CloseAllTabs));
        }

        public string ShellTitle
        {
            get => Application.Current.MainWindow.Title;
            set
            {

                Application.Current.Dispatcher.InvokeIfNeeded(() =>
                {
                    Application.Current.MainWindow.Title =  value;
                });
            }
        }

        internal WorkitemCollection Collection;

        public event NotifyCollectionChangedEventHandler WorkitemCollectionChanged
        {
            add
            {
                Collection.CollectionChanged += value;
            }
            remove
            {
                Collection.CollectionChanged -= value;
            }
        }

        public void FocusWorkitem(IWorkItem workItem)
        {
            WorkitemFocusStrategy.GetFocusStrategy(this, workItem).Focus();
        }

        public void CloseAllTabs()
        {
            bool canCloseAll = Collection.All(w => !w.IsDirty);
            MessageBoxResult res = MessageBoxResult.Yes;
            if (!canCloseAll)
                res = UIHelper.ShowMessageBox("Some tabs have unsaved changes, do you want to close all tabs?", "Closing", System.Windows.MessageBoxButton.YesNoCancel);
            else if (!UIHelper.AskForConfirmation("Do you confirm to close all tabs?"))
                return;
            if (res == MessageBoxResult.Yes)
            {
                while (Collection.Count != 0) 
                    ForceCloseWorkitem(Collection[0]);
            }
            else if(res == MessageBoxResult.No)
            {
                for (int i = 0; i < Collection.Count; i++)
                {
                    if (!Collection[i].IsDirty) {
                        ForceCloseWorkitem(Collection[i]);
                        i--;
                    }
                }
            }
        }

        public void LaunchWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem
        {
            LaunchWorkItem(typeof(T), parent, data);
        }


        void LaunchWorkItem(Type type, IWorkItem parent, object data)
        {
            IWorkItem workitem = (IWorkItem)Container.Resolve(type);
            WorkitemLaunchStrategy.GetLaunchStrategy(this, workitem, parent, data).Launch();
        }

        internal void ForceCloseWorkitem(IWorkItem workitem)
        {
            WorkitemCloseStrategy.GetCloseStrategy(this, workitem).Close();
        }

        public bool CloseWorkitem(IWorkItem workitem)
        {
            if (workitem.IsOpen)
            {
                bool canClose = true;
                if (workitem.IsDirty)
                {
                    canClose = UIHelper.AskForConfirmation("Closing the workitem may result in unsaved changes. Do you want to close it?");
                }
                if (canClose)
                {
                    ForceCloseWorkitem(workitem);
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
