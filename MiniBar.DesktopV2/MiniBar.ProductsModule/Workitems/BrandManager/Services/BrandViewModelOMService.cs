using AutoMapper;
using MiniBar.Common.Services;
using MiniBar.EntityViewModels.Products;
using MiniBar.ProductsModule.Services;
using SharedEntities.DTO.Product;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace MiniBar.ProductsModule.Workitems.BrandManager.Services
{
    class BrandViewModelOMService : IObjectManagementService<BrandViewModel, BrandUplaodViewModel>
    {
        BrandService BrandService;
        IMapper Mapper;

        public BrandViewModelOMService(BrandService brandService, IMapper mapper)
        {
            BrandService = brandService;
            Mapper = mapper;
        }

        public async Task<int> Add(BrandUplaodViewModel vm, CancellationToken token = default(CancellationToken))
        {
            BrandUploadDTO dto = new BrandUploadDTO
            {
                ID = vm.ID,
                Name = vm.Name,
                Image = vm.Image.Bytes
            };

            return await BrandService.Add(dto);
        }

        public async Task<List<BrandViewModel>> GetAll(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<BrandViewModel>>(await BrandService.GetAll());
        }

        public async Task<BrandUplaodViewModel> GetForUploadByID(int id, CancellationToken token = default(CancellationToken))
        {
            var dto = await BrandService.GetForUploadByID(id, token);
            BrandUplaodViewModel uplaodViewModel = new BrandUplaodViewModel()
            {
                ID = dto.ID,
                Name = dto.Name
            };

            if (dto.ImagePath != null)
            {
                uplaodViewModel.Image.Bytes = await Infrastructure.Utility.ImageHelper.DownloadBytesAsync(ConfigurationManager.AppSettings["ProductCdn"] + dto.ImagePath, token);
                uplaodViewModel.Image.AcceptChanges();
            }
            return uplaodViewModel;
        }

        public async Task Remove(int id, CancellationToken token = default(CancellationToken))
        {
            await BrandService.Remove(id);
        }

        public async Task Update(BrandUplaodViewModel vm, CancellationToken token = default(CancellationToken))
        {
            BrandUploadDTO dto = new BrandUploadDTO
            {
                ID = vm.ID,
                Name = vm.Name,
            };

            if (vm.Image.IsChanged)
            {
                dto.Image = vm.Image.Bytes;
                dto.ImageChanged = true;
            }

            await BrandService.Update(dto);
        }
    }
}
