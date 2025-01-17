﻿using AutoMapper;
using Infrastructure.Interface;
using Infrastructure.Utility;
using MiniBar.Common.Services;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using MiniBar.ProductsModule.Workitems.CatgeoryManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Workitems.CategoryManager.Views
{
    partial class CategoryManagerViewModel : ObjectManagerViewModel<CategoryViewModel, CategoryUploadViewModel>
    {
        private IContextService CurrentContextService;

        CategoryService CategoryService { get; set; }
        IMapper Mapper;

        IObjectManagementService<CategoryViewModel, CategoryUploadViewModel> objectManagementService;
        protected override IObjectManagementService<CategoryViewModel, CategoryUploadViewModel> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = new CategoryViewModelOMService(CategoryService, Mapper);
                return objectManagementService;
            }
        }

        protected override CategoryUploadViewModel CreateEmptyDetails()
        {
            return new CategoryUploadViewModel();
        }


        public CategoryManagerViewModel(CategoryService categoryService, IContextService currentContextService, IMapper mapper) : base()
        {
            CurrentContextService = currentContextService;
            CategoryService = categoryService;
            Mapper = mapper;
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
            return new CategoryUploadViewModel()
            {
                Names = CurrentItemDetails.Names,
                ParentCategory = CurrentItemDetails.ParentCategory,
                ParentID = CurrentItemDetails.ParentID,
            };
        }
    }
}