using Core.Documents.Adapter;
using Core.Documents.Editors;
using Core.Documents.Excel;
using DevExpress.Xpf.Ribbon;
using Infrastructure.Connection;
using Infrastructure.Util;
using MiniBar.Common.Workitems;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Resources;
using MiniBar.ProductsModule.Services;
using MiniBar.ProductsModule.Workitems.CategoryManager.Views;
using MiniBar.ProductsModule.Workitems.CategoryQC;
using Prism.Ioc;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Unity.Attributes;

namespace MiniBar.ProductsModule.Workitems.CategoryManager
{
    class CategoryManagerWorkitem : ObjectManagerWorkitem<CategoryManagerView, CategoryViewModel, CategoryUploadViewModel>
    {
        public CategoryManagerWorkitem(IContainerExtension container) : base(container)
        {
        }


        protected override string ImportTemplateName
        {
            get
            {
                return "CategoryImportTemplate";
            }
        }


        CategoryManagerViewModel CategoryManagerViewModel
        {
            get
            {
                return ObjectManagerViewModel as CategoryManagerViewModel;
            }
        }

        public override void Run()
        {
            base.Run();
            Resource("RootCategories", () => CategoryManagerViewModel.RootCategories);
        }

        public override string WorkItemName => "Category Manager";
        
        protected override RibbonPageCategory GetRibbonCategory()
        {
            return new CategoryManagerRibbonCategory();
        }

        [Dependency]
        public CategoryService CategoryService { get; set; }

        protected override ExcelDocument<CategoryUploadViewModel> GetDocument()
        {
            
            DocumentAdapter<CategoryUploadViewModel> documentAdapter = new DocumentAdapter<CategoryUploadViewModel>();
            documentAdapter.MultiColumn(
                p => p.Names,
                "Names",
                new List<string>() { "en", "it" }
            );
            documentAdapter.Column(
                p => p.ParentID,
                p => p.ParentCategory,
                "Parent Category",
                new CellListEditor<CategoryViewModel>(
                        new List<CategoryViewModel>(CategoryManagerViewModel.RootCategories),
                        c => c.ID,
                        c => c.Name
                    )
                );
            return new ExcelDocument<CategoryUploadViewModel>(documentAdapter);
        }

        protected override void LaunchQCWorkitem(List<CategoryUploadViewModel> details)
        {


            CurrentContextService.LaunchWorkItem<CategoryQCWorkitem>(details, this);
        }

        protected override IObservable<System.Reactive.Unit> AddList(List<CategoryUploadViewModel> list)
        {

            List<CategoryUploadDTO> dtos = new List<CategoryUploadDTO>();

            foreach (var vm in list)
            {
                CategoryUploadDTO dto = new CategoryUploadDTO
                {
                    ID = vm.ID,
                    Names = vm.Names.ToDictionary(),
                    ParentID = vm.ParentID
                };
                dtos.Add(dto);
            }
            return Observable.FromAsync(() => CategoryService.InsertChanges(dtos, new List<CategoryUploadDTO>(), new List<CategoryUploadDTO>()));
            
        }
    }
}
