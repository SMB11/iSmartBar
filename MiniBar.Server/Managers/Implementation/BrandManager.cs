using Facade.Managers;
using Facade.Repository;
using Managers.Base;
using SharedEntities.DTO.Product;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Facade.Accessors;
using System.Linq;
using BusinessEntities.Products;
using System.Globalization;
using Common.Core;
using Common.ResponseHandling;
using Common.Validation;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Common.Enums;

namespace Managers.Implementation
{
    public class BrandManager : ManagerBase, IBrandManager
    {
        public BrandManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<List<BrandDTO>> GetAll()
        {

            IBrandRepository brandRepo = ServiceProvider.GetService<IBrandRepository>();
            return (await brandRepo.LoadWith(b => b.Image).GetAllAsync()).Select(b => new BrandDTO
            {
                ID = b.ID,
                Name = b.Name,
                ImagePath = b.Image?.Path
            }).ToList();
        }

        public async Task<BrandDTO> GetByIDAsync(int id)
        {
            IBrandRepository brandRepo = ServiceProvider.GetService<IBrandRepository>();
            Brand brand = await brandRepo.LoadWith(b => b.Image).FindByIDAsync(id);
            BrandDTO dto = new BrandDTO
            {
                ID = brand.ID,
                Name = brand.Name,
                ImagePath = brand.Image?.Path
            };
            return dto;
        }

        public async Task<List<BrandDTO>> GetCategoryBrands(int id)
        {
            IBrandAccessor brandAccessor = ServiceProvider.GetService<IBrandAccessor>();
            List<BrandDTO> res = new List<BrandDTO>();
            foreach(Brand brand in brandAccessor.GetCategoryBrands(id))
            {
                res.Add(await GetByIDAsync(brand.ID));
            }
            return res;
        }


        public async Task<Dictionary<string, List<BrandDTO>>> GetRootCategoryBrandsWithSubcategories(int id)
        {
            IBrandAccessor brandAccessor = ServiceProvider.GetService<IBrandAccessor>();
            ICategoryManager categoryManager = ServiceProvider.GetService<ICategoryManager>();
            CategoryDTO cat = await categoryManager.GetByID(id);
            int rootId = 0;
            if (cat.ParentID.HasValue) rootId = cat.ParentID.Value;
            else rootId = cat.ID;
            List<CategoryDTO> subCategories = await categoryManager.GetSubcategoriesAsync(id);
            Dictionary<string, List<BrandDTO>> res = new Dictionary<string, List<BrandDTO>>();
            foreach(CategoryDTO category in subCategories)
            {
                res.Add(category.Name, new List<BrandDTO>());
                foreach (Brand brand in brandAccessor.GetCategoryBrands(category.ID))
                {
                    res[category.Name].Add(await GetByIDAsync(brand.ID));
                }
            }
            return res;
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<int> InsertAsync(BrandUploadDTO brand)
        {
            IDValidator.AssureEmpty(brand.ID);
            IBrandRepository brandRepo = ServiceProvider.GetService<IBrandRepository>();
            IImageManager imageManager = ServiceProvider.GetService<IImageManager>();
            Brand toAdd = new Brand { Name = brand.Name };
            if(brand.Image != null)
                toAdd.ImageID = (await imageManager.InsertBytesAsync(brand.Image)).ID;
            Brand saved = await brandRepo.InsertAsync(toAdd);

            return saved.ID;
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task UpdateAsync(BrandUploadDTO brand)
        {
            IDValidator.AssureID(brand.ID);
            IBrandRepository brandRepo = ServiceProvider.GetService<IBrandRepository>();
            IImageManager imageManager = ServiceProvider.GetService<IImageManager>();
            Brand old = (await brandRepo
                .LoadWith(b => b.Image)
                .FindByIDAsync(brand.ID));

            if (old == null)
                throw new ApiException(FaultCode.InvalidID);
            Brand toUpd = new Brand { ID = brand.ID, Name = brand.Name };
            if (brand.Image != null)
            {
                if (old.Image != null)
                    toUpd.ImageID = (await imageManager.UpdateBytesAsync(brand.Image, old.Image.Path)).ID;
                else
                    toUpd.ImageID = (await imageManager.InsertBytesAsync(brand.Image)).ID;
            }
            await brandRepo.UpdateAsync(toUpd);

        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task RemoveAsync(int id)
        {
            IDValidator.AssureID(id);
            IBrandRepository brandRepo = ServiceProvider.GetService<IBrandRepository>();
            IImageManager imageManager = ServiceProvider.GetService<IImageManager>();
            Brand old = (await brandRepo
                .LoadWith(b => b.Image)
                .FindByIDAsync(id));

            if (old == null)
                throw new ApiException(FaultCode.InvalidID);

            await brandRepo.RemoveAsync(new Brand { ID = id });
            await imageManager.RemoveAsync(old.Image);

        }

    }
}
