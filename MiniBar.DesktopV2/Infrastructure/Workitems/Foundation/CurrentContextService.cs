using Infrastructure.Constants;
using Infrastructure.Utility;
using Infrastructure.Interface;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Linq;
using System.Windows;
using Infrastructure.Workitems.Strategies.Launch;
using Infrastructure.Workitems.Strategies.Close;
using Infrastructure.Workitems.Strategies.Focus;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using Infrastructure.Framework;
using Infrastructure.Framework;

namespace Infrastructure.Workitems
{
    public class ContextService : IContextService
    {

        private IContainerExtension Container { get; set; }

        internal IRegionManager RegionManager { get; set; }

        internal IShell Shell { get; set; }

        internal IUIManager UIManager { get; set; }

        internal ITaskManager TaskManager { get; set; }


        public ContextService(IRegionManager regionManager, IShell shell, IContainerExtension container, IUIManager uiManager, ITaskManager taskManager)
        {
            Shell = shell;
            Container = container;
            RegionManager = regionManager;
            UIManager = uiManager;
            TaskManager = taskManager;
            Collection = new WorkitemCollection();
            ShellTitle = ConfigurationManager.AppSettings.Get("Title");
            FocusCommand.AllowMultipleExecution = true;
            CommandManager.RegisterCommand(CommandNames.FocusWorkitem, FocusCommand);
            CommandManager.RegisterCommand(CommandNames.CloseAllTabs, new AsyncCommand(CloseAllTabs));
        }

        private AsyncCommand<IWorkItem> focusCommand;
        public AsyncCommand<IWorkItem> FocusCommand =>   
            focusCommand ?? (focusCommand = new AsyncCommand<IWorkItem>(FocusWorkitem));
        

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

        public async Task FocusWorkitem(IWorkItem workItem)
        {
            await WorkitemFocusStrategy.GetFocusStrategy(this, workItem).Focus().ConfigureAwait(false);
        }

        public async Task CloseAllTabs()
        {
            await TaskManager.Run(async () =>
            {
                bool canCloseAll = Collection.All(w => !w.IsDirty);
                MessageBoxResult res = MessageBoxResult.Yes;
                if (!canCloseAll)
                    res = UIManager.ShowMessageBox("Some tabs have unsaved changes, do you want to close all tabs?", "Closing", System.Windows.MessageBoxButton.YesNoCancel);
                else if (!UIManager.AskForConfirmation("Do you confirm to close all tabs?"))
                    return;
                if (res == MessageBoxResult.Yes)
                {
                    while (Collection.Count != 0)
                        await ForceCloseWorkitem(Collection[0]).ConfigureAwait(false);
                }
                else if (res == MessageBoxResult.No)
                {
                    for (int i = 0; i < Collection.Count; i++)
                    {
                        if (!Collection[i].IsDirty)
                        {
                            await ForceCloseWorkitem(Collection[i]).ConfigureAwait(false);
                            i--;
                        }
                    }
                }
            }).ConfigureAwait(false);
        }

        public Task<IObservable<WorkitemEventArgs>> LaunchWorkItem<T>(object data = null, IWorkItem parent = null) where T : IWorkItem
        {
            return LaunchWorkItem(typeof(T), parent, data, false);
        }

        public Task<IObservable<WorkitemEventArgs>> LaunchWorkItem(Type type, object data = null, IWorkItem parent = null)
        {
            return LaunchWorkItem(type, parent, data, false);
        }

        public Task<IObservable<WorkitemEventArgs>> LaunchModalWorkItem<T>(IModalOption metadata, object data = null, IWorkItem parent = null) where T : IWorkItem
        {
            return LaunchWorkItem(typeof(T), parent, data, true, metadata);
        }

        public Task<IObservable<WorkitemEventArgs>> LaunchModalWorkItem<T>( object data = null, IWorkItem parent = null) where T : IWorkItem
        {
            return LaunchWorkItem(typeof(T), parent, data, true);
        }

        async Task<IObservable<WorkitemEventArgs>> LaunchWorkItem(Type type, IWorkItem parent, object data, bool isModal, IModalOption metadata = null)
        {
            if (!typeof(IWorkItem).IsAssignableFrom(type))
                throw new ArgumentException("Workitem to be launched must be of type IWorkItem");
            IWorkItem workitem = (IWorkItem)Container.Resolve(type);
            WorkitemLaunchStrategy strategy = WorkitemLaunchStrategy.GetLaunchStrategy(this, workitem, parent, data);
            if (isModal)
                return await strategy.LaunchModal(metadata).ConfigureAwait(false);
            else
                return await strategy.Launch().ConfigureAwait(false);

        }

        internal async Task ForceCloseWorkitem(IWorkItem workitem)
        {
            await WorkitemCloseStrategy.GetCloseStrategy(this, workitem).Close().ConfigureAwait(false);
        }

        public async Task<bool> CloseWorkitem(IWorkItem workitem)
        {
            if (workitem.IsOpen)
            {
                bool canClose = true;
                if (workitem.IsDirty)
                {
                    canClose = UIManager.AskForConfirmation("Closing the workitem may result in unsaved changes. Do you want to close it?");
                }
                if (canClose)
                {
                    await ForceCloseWorkitem(workitem);
                    return true;
                }
                return false;
            }
            return true;
        }

        public void BeginLoading()
        {
            Shell.ShowLoading("Loading...");
        }

        public void EndLoading()
        {
            Shell.EndLoading();
        }
    }
}
