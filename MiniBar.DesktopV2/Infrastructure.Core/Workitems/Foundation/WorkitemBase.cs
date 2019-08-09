using Infrastructure.Interface;
using Infrastructure.Utility;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Infrastructure.Workitems
{

    public abstract class WorkitemBase : IWorkItem, IDisposableContainer
    {

        #region Private
        private IViewContainer viewContainer;
        private IContainerExtension _container;
        private ISubject<WorkitemEventArgs> communicationChannel;
        IConfiguration configuration;
        private bool _isFocused;
        private Dictionary<string, Func<object>> Resources;
        private List<IDisposable> Disposables;

        private void HandleFocused()
        {
            if (Window != null)
                Window.Focus();
            OnFocused();
        }

        private void HandleLostFocus()
        {
            OnLostFocus();
        }

        #endregion

        #region Properties
        

        protected IObserver<WorkitemEventArgs> Communication => communicationChannel;

        internal IViewContainer ViewContainer => viewContainer;
        /// <summary>
        /// Shows if workitme has unsaved changes.
        /// If overriden you must take care of calling OnIsDirtyChanged
        /// every time IsDirty changes
        /// </summary>
        public virtual bool IsDirty => false;

        /// <summary>
        /// Specifies if the workitem is opened
        /// </summary>
        public bool IsOpen { get; set; } = false;

        /// <summary>
        /// The name of the Workitem, for example Product Manager
        /// </summary>
        public abstract string WorkItemName { get; }

        /// <summary>
        /// Unique ID of the workitem
        /// </summary>
        public string WorkItemID
        {
            get;
            private set;
        }

        /// <summary>
        /// The Window workitem is displayed in
        /// Window is set only on workitems opened in modal mode
        /// </summary>
        public IWindow Window { get; set; }

        public virtual event EventHandler<EventArgs> IsDirtyChanged;
        
        /// <summary>
        /// Workitems Parent in the Workitem Tree
        /// </summary>
        public IWorkItem Parent { get; set; }
        
        public IConfiguration Configuration => configuration;

        /// <summary>
        /// Is the workitem opened in modal mode
        /// </summary>
        public bool IsModal { get; private set; }

        protected IContextService CurrentContextService { get; set; }

        public IContainerExtension Container
        {
            get
            {
                return _container;
            }
        }

        #endregion

        #region Constructor

        public WorkitemBase(IContainerExtension container)
        {
            _container = container;
            CurrentContextService = container.Resolve<IContextService>();
            Resources = new Dictionary<string, Func<object>>();
            configuration = new WorkitemConfiguration();
        }

        #endregion

        #region Public/Protected Methods

        protected abstract IViewContainer CreateViewContainer();

        protected abstract ICommandContainer CreateCommandContainer();

        protected virtual void AfterWorkitemRun() { }

        protected virtual void BeforeWorkitemRun() { }

        /// <summary>
        /// Close the workitem
        /// </summary>
        /// <returns></returns>
        public Task<bool> Close()
        {
            return CurrentContextService.CloseWorkitem(this);
        }

        /// <summary>
        /// Specifies the default way to open the workitem in modal mode
        /// </summary>
        /// <returns></returns>
        public virtual void Configure()
        {
        }


        /// <summary>
        /// Launch the workitem
        /// </summary>
        public async Task<IObservable<WorkitemEventArgs>> Run()
        {
            // WorkitemID must be set before any operation takes place
            WorkItemID = Guid.NewGuid().ToString();
            Disposables = new List<IDisposable>();

            var channel = new WorkitemCommunicationChannel();
            communicationChannel = channel;

            BeforeWorkitemRun();

            IsOpen = true;
            await Create().ConfigureAwait(false);
            
            viewContainer = CreateViewContainer();
            
            Disposable(viewContainer);
            RegisterViews(viewContainer);

            ICommandContainer container = CreateCommandContainer();
            Disposable(container);
            RegisterCommands(container);

            AfterWorkitemRun();

            return communicationChannel;
        }

        /// <summary>
        /// Launch the workitem in modal mode
        /// </summary>
        public Task<IObservable<WorkitemEventArgs>> RunModal()
        {
            IsModal = true;
            return Run();
        }

        public void ChangeToModalState()
        {
            IsModal = true;
            var oldContainer = viewContainer;
            viewContainer = CreateViewContainer();
            viewContainer.ImportFrom(oldContainer);

        }

        /// <summary>
        /// Get a resource if it exists
        /// </summary>
        /// <param name="name">The name of the resource</param>
        /// <returns>The resource if found null otherwise</returns>
        public object RequestResource(string name)
        {
            if (Resources.ContainsKey(name))
                return Resources[name]?.Invoke();
            return null;
        }
        
        /// <summary>
        /// Notifies that IsDirty has changed.
        /// Should be called every time IsDirty chnages if ovveriding IsDirty
        /// </summary>
        protected void OnIsDirtyChanged()
        {
            IsDirtyChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Registeres an IDisposable obejct as Disposable
        /// note that all Disposables will be disposed during cleanup
        /// </summary>
        /// <param name="disposable"></param>
        public T Disposable<T>(T disposable) 
            where T : IDisposable
        {
            Disposables.Add(disposable);
            return disposable;
        }

        /// <summary>
        /// Registeres a new resource
        /// </summary>
        /// <param name="name"></param>
        /// <param name="getter"></param>
        protected void Resource(string name, Func<object> getter)
        {
            Resources.Add(name, getter);
        }

        /// <summary>
        /// Register all commands the workitem wants to expose
        /// This method is part of the launch process and shouldn't be invoked
        /// </summary>
        /// <param name="container"></param>
        protected virtual void RegisterCommands(ICommandContainer container)
        {
        }

        /// <summary>
        /// Regitster views
        /// This method is part of the launch process and shouldn't be invoked
        /// </summary>
        /// <param name="container"></param>
        protected virtual void RegisterViews(IViewContainer container)
        {
        }
        
        protected virtual Task Create()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when workitem loses focus
        /// </summary>
        protected virtual void OnLostFocus()
        {
        }

        /// <summary>
        /// Called when workitem is focused
        /// </summary>
        protected virtual void OnFocused()
        {
        }


        public override bool Equals(object obj)
        {
            IWorkItem workItem = obj as IWorkItem;
            if (workItem == null)
                return false;

            return workItem.WorkItemID == this.WorkItemID;
        }

        public override int GetHashCode()
        {
            var hashCode = 310520135;
            hashCode = hashCode * -1521134295 + IsDirty.GetHashCode();
            hashCode = hashCode * -1521134295 + IsOpen.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WorkItemName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WorkItemID);
            hashCode = hashCode * -1521134295 + ((ISupportsFocus)this).IsFocused.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IWorkItem>.Default.GetHashCode(Parent);
            hashCode = hashCode * -1521134295 + IsModal.GetHashCode();
            return hashCode;
        }

        #endregion

        #region IDisposable Implementation
        
        /// <summary>
        /// Dispose of the workitem
        /// </summary>
        public void Dispose()
        {
            Disposables.ForEach(d => d?.Dispose());
            DispisableHelper.DisposeEvent(IsDirtyChanged);
            communicationChannel = null;
        }
        
        #endregion

        #region ISupportFocus Implementation

        bool ISupportsFocus.IsFocused
        {
            get
            {
                return _isFocused;
            }
            set
            {
                if (value != _isFocused || IsModal)
                {
                    _isFocused = value;
                    if (value == true)
                        HandleFocused();
                    else
                        HandleLostFocus();
                }
            }
        }

        #endregion
    }
}
