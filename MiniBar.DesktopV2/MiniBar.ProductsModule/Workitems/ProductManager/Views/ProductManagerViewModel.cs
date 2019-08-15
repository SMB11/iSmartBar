using AutoMapper;
using Infrastructure.Interface;
using Infrastructure.Utility;
using MiniBar.Common.Services;
using MiniBar.Common.Workitems.ObjectManager;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using MiniBar.ProductsModule.Workitems.ProductManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Views
{
    partial class ProductManagerViewModel : ObjectManagerViewModel<ProductViewModel, ProductUploadViewModel>
    {
        ProductService ProductService { get; set; }
        CategoryService CategoryService { get; set; }
        BrandService BrandService { get; set; }
        IMapper Mapper;
        IContextService CurrentContextService;

        public ProductManagerViewModel(ProductService productService, CategoryService categoryService, BrandService brandService, IContextService currentContextService, IMapper mapper) : base()
        {
            CategoryService = categoryService;
            BrandService = brandService;
            ProductService = productService;
            CurrentContextService = currentContextService;
            Mapper = mapper;
            Initialize();
        }

        protected override async Task RefreshList()
        {
            if (Brands == null || ChildCategories == null)
            {

                IsListLoading = true;
                IsObjectLoading = true;

                await Task.WhenAll(
                    LoadBrands(),
                    LoadCategories(),
                    base.RefreshList()
                    );

                IsListLoading = false;
                IsObjectLoading = false;
            }
            else
                await base.RefreshList();
        }

        protected override async void Initialize()
        {

            IsListLoading = true;
            IsObjectLoading = true;

            await Task.WhenAll(
                LoadProducts(),
                LoadBrands(),
                LoadCategories()
                );

            IsListLoading = false;
            IsObjectLoading = false;
        }

        private async Task LoadProducts()
        {
            try
            {
                ListItems = Mapper.Map<ObservableCollection<ProductViewModel>>(await ProductService.GetAll());

            }
            catch (Exception ex)
            {
                ApiHelper.HandleApiException(ex);
            }
        }

        private async Task LoadCategories()
        {
            try
            {
                ChildCategories = Mapper.Map<List<CategoryViewModel>>((await CategoryService.GetAll()).Where(c => c.ParentID.HasValue));

            }
            catch (Exception ex)
            {
                ApiHelper.HandleApiException(ex);
            }
        }

        private async Task LoadBrands()
        {
            try
            {
                Brands = Mapper.Map<List<BrandViewModel>>(await BrandService.GetAll());

            }
            catch (Exception ex)
            {
                ApiHelper.HandleApiException(ex);
            }
        }

        private List<CategoryViewModel> categories;
        public List<CategoryViewModel> ChildCategories
        {
            get { return categories; }
            set
            {
                SetProperty(ref categories, value, nameof(ChildCategories));
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

        IObjectManagementService<ProductViewModel, ProductUploadViewModel> objectManagementService;
        protected override IObjectManagementService<ProductViewModel, ProductUploadViewModel> ObjectManagementService
        {
            get
            {
                if (objectManagementService == null)
                    objectManagementService = new ProductViewModelOMService(ProductService, Mapper);
                return objectManagementService;
            }
        }

        public bool IsValidCategoryID(int categoryID)
        {
            CategoryViewModel cat = ChildCategories.Find((c) => c.ID == categoryID);
            return cat != null && cat.ParentID != null;
        }

        protected override ProductUploadViewModel CreateCopyDetails()
        {
            return new ProductUploadViewModel()
            {
                Names = CurrentItemDetails.Names,
                Description = CurrentItemDetails.Description,
                BrandID = CurrentItemDetails.BrandID,
                CategoryID = CurrentItemDetails.CategoryID,
                Category = CurrentItemDetails.Category,
                Brand = CurrentItemDetails.Brand,
                Price = CurrentItemDetails.Price,
                Size = CurrentItemDetails.Size
            };
        }
    }
}