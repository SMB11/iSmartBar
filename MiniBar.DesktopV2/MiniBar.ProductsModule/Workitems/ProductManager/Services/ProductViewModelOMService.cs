using AutoMapper;
using MiniBar.Common.Services;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using SharedEntities.DTO.Product;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Workitems.ProductManager.Services
{
    class ProductViewModelOMService : IObjectManagementService<ProductViewModel, ProductUploadViewModel>
    {
        ProductService ProductService;
        IMapper Mapper;

        public ProductViewModelOMService(ProductService productService, IMapper mapper)
        {
            ProductService = productService;
            Mapper = mapper;
        }

        public async Task<int> Add(ProductUploadViewModel vm, CancellationToken token = default(CancellationToken))
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
                Size = vm.Size,
                Image = vm.Image.Bytes
            };

            return await ProductService.Add(dto);
        }

        public async Task<List<ProductViewModel>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<ProductViewModel>>(await ProductService.GetAll());
        }

        public async Task<ProductUploadViewModel> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {
            var dto = await ProductService.GetForUploadByID(id, token);
            Dictionary<string, string> names = new Dictionary<string, string>();
            Dictionary<string, string> descriptions = new Dictionary<string, string>();
            foreach (var info in dto.Info)
            {
                names.Add(info.Key, info.Value.Name);
                descriptions.Add(info.Key, info.Value.Description);
            }
            var vm = new ProductUploadViewModel()
            {
                ID = dto.ID,
                BrandID = dto.BrandID,
                CategoryID = dto.CategoryID,
                Price = dto.Price,
                Names = names,
                Description = descriptions,
                Size = dto.Size
            };

            if (dto.ImagePath != null)
            {
                vm.Image.Bytes = await Infrastructure.Utility.ImageHelper.DownloadBytesAsync(dto.ImagePath, token);
                vm.Image.AcceptChanges();
            }

            return vm;
        }

        public async Task Remove(int id, CancellationToken token = default(CancellationToken))
        {
            await ProductService.Remove(id);
        }

        public async Task Update(ProductUploadViewModel vm, CancellationToken token = default(CancellationToken))
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

            if (vm.Image.IsChanged)
            {
                dto.Image = vm.Image.Bytes;
                dto.ImageChanged = true;
            }

            await ProductService.Update(dto);
        }
    }
}
