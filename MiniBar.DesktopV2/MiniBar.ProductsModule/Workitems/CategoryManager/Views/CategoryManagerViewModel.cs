using System;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using AutoMapper;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Enums;
using System.Linq;
using System.Windows;
using SharedEntities.DTO.Product;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using Infrastructure.MVVM;
using Infrastructure.Connection;
using Infrastructure.MVVM.Actions;
using System.Collections.Generic;
using MiniBar.Common.MVVM;
using MiniBar.Common.Services;
using MiniBar.ProductsModule.Workitems.CatgeoryManager.Services;

namespace MiniBar.ProductsModule.Workitems.CategoryManager.Views
{
    partial class CategoryManagerViewModel : ObjectManagerViewModel<CategoryViewModel, CategoryUploadViewModel>
    {
        CategoryService CategoryService { get; set; }

        IObjectManagementService<CategoryViewModel, CategoryUploadViewModel> objectManagementService;
        protected override IObjectManagementService<CategoryViewModel, CategoryUploadViewModel> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = new CategoryViewModelOMService(CategoryService);
                return objectManagementService;
            }
        }

        protected override CategoryUploadViewModel CreateEmptyDetails()
        {
            return new CategoryUploadViewModel();
        }


        public CategoryManagerViewModel(CategoryService categoryService) : base()
        {
            CategoryService = categoryService;
            Initialize();
        }


        protected override async Task LoadList()
        {
            try
            {
                var categories = Mapper.Map<List<CategoryViewModel>>(await CategoryService.GetAll());
                RootCategories = new ObservableCollection<CategoryViewModel>(categories.Where(c => !c.ParentID.HasValue));
                ListItems = new ObservableCollection<CategoryViewModel>(categories.Where(c => c.ParentID.HasValue));
            }
            catch (Exception ex)
            {
                ApiHelper.HandleApiException(ex);
            }
        }

        protected override CategoryUploadViewModel CreateCopyDetails()
        {
            return new CategoryUploadViewModel() {
                Names = CurrentItemDetails.Names,
                ParentCategory = CurrentItemDetails.ParentCategory,
                ParentID = CurrentItemDetails.ParentID,
            };
        }
    }
}