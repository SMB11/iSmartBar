using DevExpress.Xpf.Ribbon;
using Infrastructure.Constants;
using Infrastructure.Interface;
using Infrastructure.Prism;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using Unity.Attributes;

namespace Infrastructure.Workitems
{

    public abstract class WorkitemBase : IWorkItem
    {
        #region Commands

        private DelegateCommand _closeCommand;
        protected DelegateCommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new DelegateCommand(() => Close());
                return _closeCommand;
            }
        }

        #endregion

        Dictionary<string, Func<object>> Resources;

        #region Properties

        private List<IDisposable> Disposables { get; set; }

        public virtual bool IsDirty => false;

        public bool IsOpen { get; set; } = false;

        public abstract string WorkItemName { get; }

        public string WorkItemID
        {
            get;
            private set;
        }

        private IContainerExtension _container { get; }
        public IContainerExtension Container
        {
            get
            {
                return _container;
            }
        }


        private bool _isFocused;

        public virtual event EventHandler<EventArgs> IsDirtyChanged;

        bool ISupportsFocus.IsFocused
        {
            get
            {
                return _isFocused;
            }
            set
            {
                if (value != _isFocused)
                {
                    _isFocused = value;
                    if (value == true)
                        OnFocused();
                    else
                        OnLostFocus();
                }
            }
        }

        [Dependency]
        protected ICurrentContextService CurrentContextService { get; set; }


        [Dependency]
        protected IRegionManager RegionManager { get; set; }

        public Window Window { get; set; }

        public IWorkItem Parent { get; set; }


        #endregion

        #region Constructor

        public WorkitemBase(IContainerExtension container)
        {
            _container = container;
            Resources = new Dictionary<string, Func<object>>();
        }

        #endregion

        #region Public/Protected Methods

        protected virtual void OnIsDirtyChanged()
        {
            IsDirtyChanged?.Invoke(this, new EventArgs());
        }

        protected void Disposable(IDisposable disposable)
        {
            Disposables.Add(disposable);
        }

        public virtual Task OnResultRecieved(IWorkItem child, object result) => Task.CompletedTask;

        public virtual void Run()
        {
            IsOpen = true;
            WorkItemID = Guid.NewGuid().ToString();
            Disposables = new List<IDisposable>();
            IViewContainer viewContainer = new WorkitemViewContainer(this);
            Disposable(viewContainer);
            RegisterViews(viewContainer);
            ICommandContainer container = new WorkitemCommandContainer(this);
            Disposable(container);
            RegisterCommands(container);

        }

        protected virtual void RegisterCommands(ICommandContainer container)
        {
            container.Register(CommandNames.CloseWorkItem, CloseCommand);
        }

        protected virtual void RegisterViews(IViewContainer container)
        {
            RibbonPageCategory pageCategory = GetRibbonCategory();
            if (pageCategory != null)
            {
                container.Register(pageCategory);
                pageCategory.Caption = WorkItemName;
                RegionManager.AddToRegion(RegionNames.RibbonRegion, pageCategory);
            }
        }

        protected virtual void OnLostFocus()
        {
            RegionManager.DeactivateWorkitem(this);
        }

        protected virtual void OnFocused()
        {

            RegionManager.ActivateWorkitem(this);
        }

        protected virtual RibbonPageCategory GetRibbonCategory()
        {
            return null;
        }

        protected void Resource(string name, Func<object> getter)
        {
            Resources.Add(name, getter);
        }

        public bool Close()
        {
            return CurrentContextService.CloseWorkitem(this);
        }

        public virtual void Cleanup()
        {
            Disposables.ForEach(d => d?.Dispose());
        }

        public override bool Equals(object obj)
        {
            IWorkItem workItem = obj as IWorkItem;
            if (workItem == null)
                return false;

            return workItem.WorkItemID == this.WorkItemID;
        }

        public object RequestResource(string name)
        {
            if (Resources.ContainsKey(name))
                return Resources[name]?.Invoke();
            return null;
        }

        #endregion
    }
}
