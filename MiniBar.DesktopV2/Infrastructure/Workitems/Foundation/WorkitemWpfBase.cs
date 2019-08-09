using DevExpress.Mvvm;
using Infrastructure.Constants;
using Infrastructure.Utility;
using Infrastructure.Interface;
using Prism.Ioc;
using Prism.Regions;
using Infrastructure.Logging;
using Infrastructure.Framework;
using Infrastructure.Framework;

namespace Infrastructure.Workitems
{

    public abstract class WorkitemWpfBase : WorkitemBase, IWorkItem
    {
        #region Commands

        /// <summary>
        /// Command to close the workitem
        /// </summary>
        private AsyncCommand _closeCommand;
        protected AsyncCommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new AsyncCommand(Close);
                return _closeCommand;
            }
        }

        #endregion

        #region Private
        #endregion

        #region Properties

        protected IRegionManager RegionManager { get; private set; }
        protected ICompositeLogger Logger { get; private set; }
        protected IUIManager UIManager { get; private set; }
        protected ITaskManager TaskManager { get; private set; }

        #endregion

        #region Constructor

        public WorkitemWpfBase(IContainerExtension container) : base(container)
        {
            RegionManager = container.Resolve<IRegionManager>();
            Logger = container.Resolve<ICompositeLogger>();
            UIManager = container.Resolve<IUIManager>();
            TaskManager = container.Resolve<ITaskManager>();
            Configuration.ConfigureDefault(new ModalOptions());
        }

        #endregion
        
        #region Public/Protected Methods
        
        protected override ICommandContainer CreateCommandContainer()
        {
            return new WorkitemCommandContainer(this);
        }

        protected override IViewContainer CreateViewContainer()
        {
            if (IsModal)
                return new ModalWorkitemViewContainer(this, RegionManager);
            else
                return new WorkitemViewContainer(this, RegionManager);

        }

        protected override void RegisterCommands(ICommandContainer container)
        {
            container.Register(CommandNames.CloseWorkItem, CloseCommand);
        }

        protected override void OnLostFocus()
        {

            if (!IsModal)
                RegionManager.DeactivateWorkitem(this);
            

        }

        protected override void OnFocused()
        {

            RegionManager.ActivateWorkitem(this);
        }
        
        #endregion
    }
}
