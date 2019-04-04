using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;
using AutoMapper;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Infrastructure.Interface;
using Infrastructure.MVVM;
using Infrastructure.MVVM.Commands;
using MiniBar.Common.Resources;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using Security.Api;
using SharedEntities.DTO.Product;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    class ProductDetailsViewModel : BaseViewModel, IObjectDetailsManager
    {

        CategoryService CategoryService { get; set; }
        BrandService BrandService { get; set; }
        ProductService ProductService { get; set; }
        public ProductDetailsViewModel(ProductService productService, CategoryService categoryService, BrandService brandService) : base()
        {
            CategoryService = categoryService;
            BrandService = brandService;
            ProductService = productService;
            LoadCategories();
            LoadBrands();
        }

        private async void LoadCategories()
        {
            try
            {
                Categories = Mapper.Map<List<CategoryViewModel>>(await CategoryService.GetAll());

            }
            catch(Exception ex)
            {
                ApiHelper.HandleApiException(ex);
            }
        }
        
        private async void LoadBrands()
        {
            try { 
                Brands = Mapper.Map<List<BrandViewModel>>(await BrandService.GetAll());

            }
            catch (Exception ex)
            {
                ApiHelper.HandleApiException(ex);
            }
        }

        private ProductUploadViewModel oldItem;
        private ProductUploadViewModel currentItem;
        public ProductUploadViewModel CurrentItem
        {
            get { return currentItem; }
            set {
                SetProperty(ref currentItem, value, nameof(CurrentItem));
            }
        }

        private List<CategoryViewModel> categories;
        public List<CategoryViewModel> Categories
        {
            get { return categories; }
            set
            {
                SetProperty(ref categories, value, nameof(Categories));
            }
        }

        private List<BrandViewModel> brands;
        public List<BrandViewModel> Brands
        {
            get { return brands; }
            set
            {
                SetProperty(ref brands, value, nameof(brands));
            }
        }

        private bool isReadOnly = true;

        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                SetProperty(ref isReadOnly, value, nameof(IsReadOnly));
                DescriptionEditCommand.RaiseCanExecuteChanged();
                NameEditCommand.RaiseCanExecuteChanged();
            }
        }

        private bool isobjectLoading;

        public bool IsObjectLoading
        {
            get { return isobjectLoading; }
            set
            {
                SetProperty(ref isobjectLoading, value, nameof(IsObjectLoading));
            }
        }

        private SecureCommand nameEditCommand;
        public SecureCommand NameEditCommand
        {
            get
            {
                if (nameEditCommand == null)
                    nameEditCommand = new SecureCommand(StartEditName, CanEdit);
                return nameEditCommand;
            }
        }


        private SecureCommand descEditCommand;
        public SecureCommand DescriptionEditCommand
        {
            get
            {
                if (descEditCommand == null)
                    descEditCommand = new SecureCommand(StartEditDescription, CanEdit);
                return descEditCommand;
            }
        }

        public bool IsValidCategoryID(int categoryID)
        {
            CategoryViewModel cat = Categories.Find((c) => c.ID == categoryID);
            return cat != null && cat.ParentID != null;
        }

        private void StartEditName()
        {
            DXDialogWindow editWindow = null;
            LanguageEdit languageEdit = new LanguageEdit();
            LanguageEditViewModel vm = (LanguageEditViewModel)languageEdit.DataContext;
            vm.SetData(currentItem.Names?? new Dictionary<string, string>());
            UICommand saveCommand = new UICommand
            {
                Caption = "Save",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>((c) => SaveName(c, languageEdit.DataContext as LanguageEditViewModel))
            };
            UICommand cancleCommand = new UICommand
            {
                Caption = "Cancel",
                IsCancel = true
            };
            editWindow = new DXDialogWindow("Edit Name", new List<UICommand> { saveCommand, cancleCommand });
            editWindow.Width = 300;
            editWindow.Height = 300;
            editWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editWindow.ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
            editWindow.Content = languageEdit;
            editWindow.SetParent(Application.Current.MainWindow);
            editWindow.ShowDialogWindow();
        }

        private void StartEditDescription()
        {
            DXDialogWindow editWindow = null;
            LanguageEdit languageEdit = new LanguageEdit();
            LanguageEditViewModel vm = languageEdit.DataContext as LanguageEditViewModel;
            vm.SetData(currentItem.Description ?? new Dictionary<string, string>());
            UICommand saveCommand = new UICommand
            {
                Caption = "Save",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>((c) => SaveDescription(c, languageEdit.DataContext as LanguageEditViewModel))
            };
            UICommand cancleCommand = new UICommand
            {
                Caption = "Cancel",
                IsCancel = true
            };
            editWindow = new DXDialogWindow("Edit Name", new List<UICommand> { saveCommand, cancleCommand });
            editWindow.Width = 300;
            editWindow.Height = 300;
            editWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editWindow.ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
            editWindow.Content = languageEdit;
            editWindow.SetParent(Application.Current.MainWindow);
            editWindow.ShowDialogWindow();
        }

        private bool CanEdit()
        {
            return !IsReadOnly;
        }

        private void SaveName(CancelEventArgs arg, LanguageEditViewModel vm)
        {
            if (!vm.Validate())
            {
                arg.Cancel = true;
            }
            else
            {
                CurrentItem.Names = vm.GetData();
            }
        }

        private void SaveDescription(CancelEventArgs arg, LanguageEditViewModel vm)
        {
            if (!vm.Validate())
            {
                arg.Cancel = true;
            }
            else
            {
                CurrentItem.Description = vm.GetData();
            }
        }

        #region IObjectDetailsManager
        object IObjectDetailsManager.EditingItem { get => (!this.IsReadOnly)? CurrentItem: null; }
        

        void IObjectDetailsManager.BeginAdd()
        {
            oldItem = CurrentItem;
            CurrentItem = new ProductUploadViewModel();
            IsReadOnly = false;
        }

        void IObjectDetailsManager.BeginEdit()
        {
            IsReadOnly = false;
        }

        void IObjectDetailsManager.ChangeCurrentItem(object currentItem)
        {
            if(currentItem == null)
            {
                CurrentItem = null;
            }
            else
            {
                IObservable<ProductUploadDTO> obs = Observable.FromAsync(() => ProductService.GetForUploadByID((currentItem as ProductViewModel).ID));
                obs.Subscribe(dto => {
                    Dictionary<string, string> names = new Dictionary<string, string>();
                    Dictionary<string, string> descriptions = new Dictionary<string, string>();
                    foreach (var info in dto.Info)
                    {
                        names.Add(info.Key, info.Value.Name);
                        descriptions.Add(info.Key, info.Value.Description);
                    }
                    CurrentItem = new ProductUploadViewModel()
                    {
                        ID = dto.ID,
                        BrandID = dto.BrandID,
                        CategoryID = dto.CategoryID,
                        Price = dto.Price,
                        Names = names,
                        Description = descriptions
                    };
                }, ex => {

                    ApiHelper.HandleApiException(ex);
                });
                
            }
            
        }

        void IObjectDetailsManager.Cancel()
        {
            if (oldItem != null)
            {
                CurrentItem = oldItem;
                oldItem = null;
            }
            IsReadOnly = true;
        }
        #endregion
    }
}
