using AutoMapper;
using MiniBar.Common.Services;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using SharedEntities.DTO.Product;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Workitems.CatgeoryManager.Services
{
    class CategoryViewModelOMService : IObjectManagementService<CategoryViewModel, CategoryUploadViewModel>
    {
        CategoryService CategoryService;
        IMapper Mapper;

        public CategoryViewModelOMService(CategoryService categoryService, IMapper mapper)
        {
            CategoryService = categoryService;
            Mapper = mapper;
        }

        public async Task<int> Add(CategoryUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {

            CategoryUploadDTO dto = new CategoryUploadDTO
            {
                Names = new Dictionary<string, string>(vm.Names),
                ParentID = vm.ParentID
            };

            return await CategoryService.Add(dto);
        }

        public async Task<List<CategoryViewModel>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<CategoryViewModel>>(await CategoryService.GetAll());
        }

        public async Task<CategoryUploadViewModel> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {
            var dto = await CategoryService.GetForUploadByID(id, token);
            return new CategoryUploadViewModel()
            {
                ID = dto.ID,
                Names = new EntityViewModels.Global.BindableDictionary<string>(dto.Names),
                ParentID = dto.ParentID
            };
        }

        public async Task Remove(int id, CancellationToken token = default(CancellationToken))
        {
            await CategoryService.Remove(id);
        }

        public async Task Update(CategoryUploadViewModel vm, CancellationToken token = default(CancellationToken))
        {
            CategoryUploadDTO dto = new CategoryUploadDTO
            {
                ID = vm.ID,
                Names = new Dictionary<string, string>(vm.Names),
                ParentID = vm.ParentID
            };

            await CategoryService.Update(dto);
        }
    }
}
