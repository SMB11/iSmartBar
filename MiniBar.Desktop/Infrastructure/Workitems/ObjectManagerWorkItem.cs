using Infrastructure.Helpers;
using Infrastructure.Interface;
using Infrastructure.Prism;
using Infrastructure.Resources;
using Prism.Ioc;
using System;
using System.Windows;

namespace Infrastructure.Workitems
{
    public abstract class ObjectManagerWorkItem<TListView, TDetailsView> : CrudManagerWorkitem
        where TListView : FrameworkElement, new()
        where TDetailsView : FrameworkElement, new()
    {
        #region Private Fields
        private TListView listView;
        private TDetailsView detailsView;
        private EditMode editMode = EditMode.Default;
        #endregion

        public EditMode EditMode
        {
            get
            {
                return editMode;
            }
            set
            {
                editMode = value;
                UpdateCrudCommands();
            }
        }

        protected IObjectListManager ObjectListManager
        {
            get;
            private set;
        }

        protected IObjectDetailsManager ObjectDetailsManager
        {
            get;
            private set;
        }
        
        public ObjectManagerWorkItem(IContainerExtension container) : base(container)
        {
        }
        

        public override void Run()
        {
            listView = Container.Resolve<TListView>();
            detailsView = Container.Resolve<TDetailsView>();
            if (listView.DataContext is IObjectListManager && detailsView.DataContext is IObjectDetailsManager)
            {
                ObjectListManager = listView.DataContext as IObjectListManager;
                ObjectDetailsManager = detailsView.DataContext as IObjectDetailsManager;
            }
            else
                throw new Exception("ViewModels must implement IManagerAware interface.");

            base.Run();

            InitViewModels();

            RegionManager.AddToRegion(Infrastructure.Constants.RegionNames.ContentRegion, new ObjectManagerView(listView, detailsView));
        }

        protected override void Add()
        {
            base.Add();
            ObjectListManager.Disable();
            ObjectDetailsManager.BeginAdd();
            EditMode = EditMode.Add;
        }

        protected override bool CanAdd()
        {
            return base.CanAdd() && editMode == EditMode.Default;
        }

        protected override void Edit()
        {
            base.Edit();
            ObjectListManager.Disable();
            ObjectDetailsManager.BeginEdit();
            EditMode = EditMode.Edit;
        }

        public override bool CanClose()
        {
            if (EditMode != EditMode.Default)
            {
                MessageBoxResult res = UIHelper.ShowMessageBox("Closing the workitem may result in unsaved changes. Do you want to close it?", "Closing", System.Windows.MessageBoxButton.YesNo);
                return res == MessageBoxResult.Yes;
            }
            return true;
        }

        public override void Cleanup()
        {
            base.Cleanup();

            RegionManager.ClearNavigation(Infrastructure.Constants.RegionNames.ContentRegion);
        }

        protected override bool CanEdit()
        {
            return base.CanEdit() && editMode == EditMode.Default;
        }

        protected override bool CanSave()
        {
            return base.CanSave() && editMode != EditMode.Default;
        }

        protected override bool CanDelete()
        {
            return base.CanDelete() && editMode == EditMode.Default;
        }

        protected override void CancelEditing()
        {
            base.CancelEditing();
            ObjectListManager.Enable();
            ObjectDetailsManager.Cancel();
            EditMode = EditMode.Default;
        }

        protected override bool CanCancelEditing()
        {
            return base.CanCancelEditing() && editMode != EditMode.Default;
        }

        protected virtual void OnCurrentItemChanged(object currentItem)
        {

        }

        private void InitViewModels()
        {
            ObjectListManager.WhenCurrentItemChanges.Subscribe(HandleCurrentItemChanged);
        }


        private void HandleCurrentItemChanged(object currentItem)
        {
            OnCurrentItemChanged(currentItem);
            ObjectDetailsManager.ChangeCurrentItem(currentItem);
        }
    }
}
