using MiniBar.ProductsModule.Workitems.ProductManager.Views;
using DXInfrastructure;
using DevExpress.Xpf.Ribbon;
using MiniBar.ProductsModule.Resources;
using Prism.Ioc;
using MiniBar.ProductsModule.Constants;
using MiniBar.ProductsModule.Services;
using MiniBar.EntityViewModels.Products;
using SharedEntities.DTO.Product;
using Infrastructure.Helpers;
using System.Collections.Generic;
using System;
using MiniBar.EntityViewModels.Interfaces;
using System.ComponentModel;
using Infrastructure.Extensions;
using Security.Api;
using Infrastructure.Workitems;
using System.Reactive;
using System.Reactive.Linq;
using Prism.Events;
using Infrastructure.Security;

namespace MiniBar.ProductsModule.Workitems.BrandManager
{
    class BrandManagerWorkitem : ObjectManagerWorkItem<BrandListView, BrandDetailsView>
    {

        #region Private Fields/Properties

        private BrandService BrandService { get; set; }

        #endregion

        #region Constructor

        public BrandManagerWorkitem(IContainerExtension container) : base(container)
        {
            BrandService = Container.Resolve<BrandService>();
            AppSecurityContext.AppPrincipalChanged += (o, e) => HandleAutheticationStateChanged();
        }


        #endregion

        #region Properties
        public override string WorkItemName => "Brand Manager";

        #endregion

        #region Private Methods

        private void HandleAutheticationStateChanged()
        {
            if (!AppSecurityContext.CurrentPrincipal.Identity.IsAuthenticated)
            {
                ObjectListManager.Disable();
                UpdateCrudCommands();
            }
            else
            {

                ObjectListManager.Enable();
                UpdateCrudCommands();
            }
        }

        #endregion

        #region Public/Protected Methods
        public override void Run()
        {
            base.Run();
            BarCommandManager.RegisterCommand(CommandNames.AddBrand, AddCommand);
            BarCommandManager.RegisterCommand(CommandNames.EditBrand, EditCommand);
            BarCommandManager.RegisterCommand(CommandNames.SaveBrand, SaveCommand);
            BarCommandManager.RegisterCommand(CommandNames.RemoveBrand, DeleteCommand);
            BarCommandManager.RegisterCommand(CommandNames.CancelEditingBrand, CancelCommand);
            ObjectListManager.RefreshItems();
        }

        protected override void OnCurrentItemChanged(object currentItem)
        {
            base.OnCurrentItemChanged(currentItem);
            UpdateCrudCommands();
        }

        protected override bool CanEdit()
        {
            return base.CanEdit() && ObjectListManager.CurrentItem != null;
        }

        protected override bool CanDelete()
        {
            return base.CanDelete() && ObjectListManager.CurrentItem != null;
        }

        protected override void Save()
        {
            base.Save();

            IDataErrorInfo dataErrorInfo = ObjectDetailsManager.EditingItem as IDataErrorInfo;
            if (dataErrorInfo.HasErrors())
            {
                UIHelper.ShowErrorMessageBox("Correct all errors before saving.");
                return;
            }

            if (EditMode == EditMode.Add)
            {

                BrandUplaodViewModel vm = ObjectDetailsManager.EditingItem as BrandUplaodViewModel;
                BrandUploadDTO dto = new BrandUploadDTO
                {
                    ID = vm.ID,
                    Image = vm.Image,
                    Name = vm.Name
                };
                ObjectDetailsManager.IsObjectLoading = true;
                IObservable<int> obs = Observable.FromAsync(() => BrandService.Add(dto));
                obs.Subscribe(id =>
                {
                    CancelEditing();
                    ObjectListManager.RefreshItems(id);
                    ObjectDetailsManager.IsObjectLoading = false;
                }, ex =>
                {
                    ApiHelper.HandleApiException(ex);
                    ObjectDetailsManager.IsObjectLoading = false;
                });

            }
            else if (EditMode == EditMode.Edit)
            {
                BrandUplaodViewModel vm = ObjectDetailsManager.EditingItem as BrandUplaodViewModel;

                BrandUploadDTO dto = new BrandUploadDTO
                {
                    ID = vm.ID,
                    Image = vm.Image,
                    Name = vm.Name
                };

                ObjectDetailsManager.IsObjectLoading = true;
                IObservable<Unit> obs = Observable.FromAsync(() => BrandService.Update(dto));
                obs.Subscribe(_ =>
                {
                    CancelEditing();
                    ObjectListManager.RefreshItems(vm.ID);
                    ObjectDetailsManager.IsObjectLoading = false;
                }, ex =>
                {
                    ApiHelper.HandleApiException(ex);
                    ObjectDetailsManager.IsObjectLoading = false;
                });

            }
        }

        protected override void Delete()
        {
            base.Delete();
            int id = (ObjectListManager.CurrentItem as IIdEntityViewModel).ID;
            ObjectListManager.IsListLoading = true;
            IObservable<Unit> obs = Observable.FromAsync(() => BrandService.Remove(id));
            obs.Subscribe(_ => {
                ObjectListManager.RemoveByID(id);
                ObjectListManager.IsListLoading = false;
            }, (e) => {
                ApiHelper.HandleApiException(e);
                ObjectListManager.IsListLoading = false;
            });
        }

        protected override RibbonPageCategory GetRibbonCategory()
        {
            return new BrandManagerRibbonPageGroup();

        }

        public override void Cleanup()
        {
            base.Cleanup();

            BarCommandManager.UnregisterCommand(CommandNames.AddBrand, AddCommand);
            BarCommandManager.UnregisterCommand(CommandNames.EditBrand, EditCommand);
            BarCommandManager.UnregisterCommand(CommandNames.SaveBrand, SaveCommand);
            BarCommandManager.UnregisterCommand(CommandNames.RemoveBrand, DeleteCommand);
            BarCommandManager.UnregisterCommand(CommandNames.CancelEditingBrand, CancelCommand);
        }

        #endregion




    }
}
