using Infrastructure.Utility;
using Infrastructure.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
using Infrastructure.Logging;
using Infrastructure.Framework;
using Infrastructure.Framework;

namespace Infrastructure.Workitems.Strategies.Launch
{
    internal abstract class WorkitemLaunchStrategy
    {
        protected IObservable<WorkitemEventArgs> Channel;
        protected ContextService CurrentContextService { get; private set; }
        protected IWorkItem Workitem { get; private set; }
        protected IWorkItem Parent { get; private set; }
        protected object Data { get; private set; }
        protected IModalOption ModalMetadata { get; private set; }
        protected ICompositeLogger Logger { get; private set; }
        protected ITaskManager TaskManager { get; private set; }
        protected IUIManager UIManager { get; private set; }
        public bool ShouldOpenModal { get; private set; }

        internal WorkitemLaunchStrategy(ContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null) {
            CurrentContextService = currentContextService;
            Workitem = workItem;
            Parent = parent;
            Data = data;
            Logger = CommonServiceLocator.ServiceLocator.Current.GetInstance<ICompositeLogger>();
            TaskManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<ITaskManager>();
            UIManager = CommonServiceLocator.ServiceLocator.Current.GetInstance<IUIManager>();
        }

        public static WorkitemLaunchStrategy GetLaunchStrategy(ContextService currentContextService, IWorkItem workItem, IWorkItem parent = null, object data = null)
        {
            if(parent != null)
                return new ChildWorkitemLaunchStrategy(currentContextService, workItem, parent, data);
            else
                return new RootWorkitemLaunchStrategy(currentContextService, workItem, parent, data);
        }

        protected abstract Task Execute();

        protected async Task RunWorkitem()
        {

            Logger.Log("Running Workitem", LogLevel.Informative);
            await Application.Current.Dispatcher.InvokeAsyncIfNeeded(async () =>
            {
                if (ShouldOpenModal)
                    Channel = await Workitem.RunModal().ConfigureAwait(false);
                else
                    Channel = await Workitem.Run().ConfigureAwait(false);
            }).ConfigureAwait(false);
            
        }

        private async Task<IObservable<WorkitemEventArgs>> LaunchInternal(IModalOption modalMetadata = null)
        {

            try
            {
                Logger.Log(String.Format("Opening workitem {0}{1}.", Workitem.WorkItemName, ShouldOpenModal ? " in modal state" : ""), LogLevel.Informative);
                await TaskManager.Run(() => Thread.Sleep(50)).ConfigureAwait(false);
                if (ShouldOpenModal)
                    CurrentContextService.BeginLoading();
                Type type = Workitem.GetType();
                SingleInstanceWorkitemAttribute attribute = type.GetCustomAttributes(typeof(SingleInstanceWorkitemAttribute), false).FirstOrDefault() as SingleInstanceWorkitemAttribute;
                if (attribute != null)
                {
                    IWorkItem exists = CurrentContextService.Collection.Where(w => w.GetType().Equals(type)).FirstOrDefault();
                    if (exists != null)
                    {
                        await CurrentContextService.FocusWorkitem(exists).ConfigureAwait(false);
                        return null;
                    }
                }

                if (Workitem is ISupportsInitialization)
                {
                    var initable = Workitem as ISupportsInitialization;
                    try
                    {
                        Logger.Log("Initializing workitem", LogLevel.Informative);
                        initable.Initialize(Data);
                    }
                    catch
                    {
                        Logger.Log("Workitem initialization failed", LogLevel.Informative);
                        UIManager.Error("Failed to Initialize Workitem");
                        return null;
                    }
                }

                Logger.Log("Configuring Workitem", LogLevel.Informative);
                Workitem.Configure();

                if (ShouldOpenModal)
                {
                    if (modalMetadata == null)
                        ModalMetadata = Workitem.Configuration.GetOption<IModalOption>();
                    else
                        ModalMetadata = modalMetadata;

                    Application.Current.Dispatcher.InvokeIfNeeded(() => Workitem.Window = WorkitemHelper.GetModalWindow(Workitem, ModalMetadata));

                }

                await Execute().ConfigureAwait(false);

                if (Workitem is NullWorkitem)
                    CurrentContextService.Collection.Null = Workitem;
                else if (!ShouldOpenModal || !ModalMetadata.IsDialog)
                    CurrentContextService.Collection.Add(Workitem);

                if (ShouldOpenModal)
                {

                    Logger.Log("Showing modal window", LogLevel.Informative);
                    if (ModalMetadata.IsDialog)
                    {
                        CurrentContextService.EndLoading();
                        Application.Current.Dispatcher.InvokeIfNeeded(() => Workitem.Window.ShowDialog());
                    }
                    else
                        Application.Current.Dispatcher.InvokeIfNeeded(() => Workitem.Window.Show());


                }

                if (!ShouldOpenModal || !ModalMetadata.IsDialog)
                    await CurrentContextService.FocusWorkitem(Workitem).ConfigureAwait(false);

                return Channel;
            }
            catch (Exception e)
            {
                CurrentContextService.Collection.Remove(Workitem);
                Workitem.Dispose();
                UIManager.Error("Failed to open workitem");
                
                return null;
            }
            finally
            {

                if (ShouldOpenModal)
                    CurrentContextService.EndLoading();
            }
        }

        public Task<IObservable<WorkitemEventArgs>> Launch()
        {
            return LaunchInternal();
        }

        public Task<IObservable<WorkitemEventArgs>> LaunchModal(IModalOption modalWorkitemMetadata)
        {
            ShouldOpenModal = true;
            return LaunchInternal(modalWorkitemMetadata);
        }
    }
}
