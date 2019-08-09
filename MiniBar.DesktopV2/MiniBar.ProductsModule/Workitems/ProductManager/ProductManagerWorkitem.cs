using MiniBar.ProductsModule.Workitems.ProductManager.Views;
using DevExpress.Xpf.Ribbon;
using Prism.Ioc;
using Infrastructure.Workitems;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Resources;
using DevExpress.Spreadsheet;
using Infrastructure.Interface;
using System.Collections.Generic;
using MiniBar.ProductsModule.Workitems.ProductQC;
using MiniBar.ProductsModule.Services;
using SharedEntities.DTO.Product;
using SharedEntities.Enum;
using System.Threading.Tasks;
using System;
using System.Reactive.Linq;
using MiniBar.Common.Workitems.ObjectManager;
using Documents.Adapter;
using Infrastructure.Office;
using Documents.Validation;
using Documents.Editors;

namespace MiniBar.ProductsModule.Workitems.ProductManager
{
    public class ProductManagerWorkitem : ObjectManagerWorkitem<ProductManagerView, ProductViewModel, ProductUploadViewModel>
    {

        public static WorkitemMetadata Metadata = new WorkitemMetadata("Product Manager", "Add/Edit/Remove Products");

        #region Constructor

        public ProductManagerWorkitem(IContainerExtension container, ProductService productService) : base(container)
        {
            ProductService = productService;
        }


        #endregion

        #region Properties
        public override string WorkItemName => Metadata.Name;

        protected override string ImportTemplateName
        {
            get
            {
                return "ProductImportTemplate";
            }
        }

        public ProductService ProductService { get; set; }
        #endregion

        ProductManagerViewModel ProductManagerViewModel
        {
            get
            {
                return ObjectManagerViewModel as ProductManagerViewModel;
            }
        }

        protected override void AfterWorkitemRun()
        {
            base.AfterWorkitemRun();
            Resource("ChildCategories", () => ProductManagerViewModel.ChildCategories);
            Resource("Brands", () => ProductManagerViewModel.Brands);
        }

        protected override ExcelDocument<ProductUploadViewModel> GetDocument()
        {
            ProductManagerViewModel productManagerViewModel = (ObjectManagerViewModel as ProductManagerViewModel);
            DocumentAdapter<ProductUploadViewModel> documentAdapter = new DocumentAdapter<ProductUploadViewModel>();
            documentAdapter.MultiColumn(
                p => p.Names, 
                "Names", 
                new List<string>() { "en", "it" }
            );
            documentAdapter.MultiColumn(
                p => p.Description, 
                "Description",
                new List<string>() { "en", "it" }
            );
            documentAdapter.Column(
                p => p.Price, "Price",
                new CellTextEditor(new DataValidationCriteria(DataValidationType.Decimal, DataValidationOperator.GreaterThan, 0))
            );

            documentAdapter.Column(
                p => p.Size, "Size",
                new CellEnumEditor<ProductSize>()
            );
            documentAdapter.Column(
                p => p.CategoryID,
                p => p.Category,
                "Category", 
                new CellListEditor<CategoryViewModel>(
                        productManagerViewModel.ChildCategories,
                        c => c.ID,
                        c => c.Name
                    )
                );
            documentAdapter.Column(
                p => p.BrandID, 
                p => p.Brand,
                "Brand",
                new CellListEditor<BrandViewModel>(
                    productManagerViewModel.Brands, 
                    c => c.ID, 
                    c => c.Name
                )
            );
            return  new ExcelDocument<ProductUploadViewModel>(documentAdapter);
        }

        protected override Task<IObservable<WorkitemEventArgs>> LaunchQCWorkitem(List<ProductUploadViewModel> res)
        {
            return CurrentContextService.LaunchModalWorkItem<ProductQCWorkitem>(res, this);
        }

        protected override IObservable<System.Reactive.Unit> AddList(List<ProductUploadViewModel> list)
        {

            List<ProductUploadDTO> dtos = new List<ProductUploadDTO>();

            foreach (var vm in list)
            {
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
                    Size = vm.Size
                };
                dtos.Add(dto);
            }

            return Observable.FromAsync(() => ProductService.AddList(dtos));
        }
        

        protected override RibbonPageCategory GetRibbonCategory()
        {
            return new ProductManagerRibbonCategory();
        }


    }
}
