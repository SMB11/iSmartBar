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

namespace MiniBar.ProductsModule.Workitems.ProductManager
{
    class ProductManagerWorkitem : ObjectManagerWorkItem<ProductListView, ProductDetailsView>
    {

        #region Private Fields/Properties

        private ProductService ProductService { get; set; }

        #endregion

        #region Constructor

        public ProductManagerWorkitem(IContainerExtension container) : base(container)
        {
            ProductService = Container.Resolve<ProductService>();
            AppSecurityContext.AppPrincipalChanged += (o, e) => HandleAutheticationStateChanged();
        }


        #endregion

        #region Properties
        public override string WorkItemName => "Product Manager";

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
            BarCommandManager.RegisterCommand(CommandNames.AddProduct, AddCommand);
            BarCommandManager.RegisterCommand(CommandNames.EditProduct, EditCommand);
            BarCommandManager.RegisterCommand(CommandNames.SaveProduct, SaveCommand);
            BarCommandManager.RegisterCommand(CommandNames.RemoveProduct, DeleteCommand);
            BarCommandManager.RegisterCommand(CommandNames.CancelEditingProduct, CancelCommand);
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

                ProductUploadViewModel vm = ObjectDetailsManager.EditingItem as ProductUploadViewModel;
                Dictionary<string, ProductLangData> langData = new Dictionary<string, ProductLangData>();
                foreach (var langName in vm.Names)
                {
                    langData.Add(langName.Key, new ProductLangData() { Name = langName.Value, Description = vm.Description[langName.Key] });
                }
                ProductUploadDTO dto = new ProductUploadDTO
                {
                    ID = vm.ID,
                    BrandID = vm.BrandID,
                    CategoryID = vm.CategoryID,
                    Price = vm.Price,
                    Info = langData,
                    Image = vm.Image,
                    Size = vm.Size
                };
                ObjectDetailsManager.IsObjectLoading = true;
                IObservable<int> obs = Observable.FromAsync(() => ProductService.Add(dto));
                obs.Subscribe(id => {
                    CancelEditing();
                    ObjectListManager.RefreshItems(id);
                    ObjectDetailsManager.IsObjectLoading = false;
                }, ex => {
                    ApiHelper.HandleApiException(ex);
                    ObjectDetailsManager.IsObjectLoading = false;
                });

            }
            else if (EditMode == EditMode.Edit)
            {
                ProductUploadViewModel vm = ObjectDetailsManager.EditingItem as ProductUploadViewModel;
                Dictionary<string, ProductLangData> langData = new Dictionary<string, ProductLangData>();
                foreach (var langName in vm.Names)
                {
                    langData.Add(langName.Key, new ProductLangData() { Name = langName.Value, Description = vm.Description[langName.Key] });
                }
                ProductUploadDTO dto = new ProductUploadDTO
                {
                    ID = vm.ID,
                    BrandID = vm.BrandID,
                    CategoryID = vm.CategoryID,
                    Price = vm.Price,
                    Info = langData,
                    Image = vm.Image,
                    Size = vm.Size
                };

                ObjectDetailsManager.IsObjectLoading = true;
                IObservable<Unit> obs = Observable.FromAsync(() => ProductService.Update(dto));
                obs.Subscribe(_ => {
                    CancelEditing();
                    ObjectListManager.RefreshItems(vm.ID);
                    ObjectDetailsManager.IsObjectLoading = false;
                }, ex => {
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
            IObservable<Unit> obs = Observable.FromAsync(() => ProductService.Remove(id));
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
            return new ProductManagerRibbonPageGroup();

        }

        public override void Cleanup()
        {
            base.Cleanup();

            BarCommandManager.UnregisterCommand(CommandNames.AddProduct, AddCommand);
            BarCommandManager.UnregisterCommand(CommandNames.EditProduct, EditCommand);
            BarCommandManager.UnregisterCommand(CommandNames.SaveProduct, SaveCommand);
            BarCommandManager.UnregisterCommand(CommandNames.RemoveProduct, DeleteCommand);
            BarCommandManager.UnregisterCommand(CommandNames.CancelEditingProduct, CancelCommand);
        }

        #endregion




    }
}
