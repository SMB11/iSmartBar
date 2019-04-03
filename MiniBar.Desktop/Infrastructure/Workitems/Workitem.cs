using DevExpress.Xpf.Ribbon;
using Infrastructure.Constants;
using Infrastructure.Interface;
using Infrastructure.Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Unity.Attributes;

namespace Infrastructure.Workitems
{
    public abstract class Workitem : IWorkItem
    {
        #region Commands
        
        private DelegateCommand _closeCommand;
        protected DelegateCommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new DelegateCommand(Close, CanClose);
                return _closeCommand;
            }
        }

        #endregion

        #region Properties

        public abstract string WorkItemName { get; }
        public virtual string WorkItemID
        {
            get
            {
                return WorkItemName.Replace(" ", "");
            }
        }

        private IContainerExtension _container{ get; }
        public IContainerExtension Container
        {
            get
            {
                return _container;
            }
        }

        [Dependency]
        protected ICurrentContextService CurrentContextService { get; set; }


        [Dependency]
        protected IRegionManager RegionManager{ get; set; }

        #endregion

        #region Constructor
        
        public Workitem(IContainerExtension container)
        {
            _container = container;
        }

        #endregion

        #region Public/Protected Methods


        public virtual void Run()
        {
            RibbonPageCategory pageCategory = GetRibbonCategory();
            pageCategory.Caption = WorkItemName;
            pageCategory.Tag = WorkItemID;
            RegionManager.AddToRegion(RegionNames.RibbonRegion, pageCategory);

        }

        protected virtual RibbonPageCategory GetRibbonCategory()
        {
            return null;
        }

        private void Close()
        {
            CurrentContextService.CloseCurrentWorkItem();
        }
        
        public virtual bool CanClose()
        {
            return true;
        }


        //public virtual void Terminate()
        //{
        //}

        public virtual void Cleanup()
        {
            RegionManager.RemoveWorkitemViews(WorkItemID);
        }

        #endregion

    }
}
