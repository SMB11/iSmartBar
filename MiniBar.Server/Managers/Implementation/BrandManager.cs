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
            return Mapper.Map<List<BrandDTO>>(await brandRepo.GetAllAsync());
        }

        public async Task<BrandDTO> GetByIDAsync(int id)
        {
            IBrandRepository brandRepo = ServiceProvider.GetService<IBrandRepository>();
            Brand brand = await brandRepo.FindByIDAsync(id);
            return Mapper.Map<BrandDTO>(brand);
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

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<int> InsertAsync(BrandDTO brand)
        {
            IDValidator.AssureEmpty(brand.ID);
            IBrandRepository brandRepo = ServiceProvider.GetService<IBrandRepository>();
            Brand saved = await brandRepo.InsertAsync(Mapper.Map<Brand>(brand));

            return saved.ID;
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task UpdateAsync(BrandDTO brand)
        {
            IDValidator.AssureID(brand.ID);
            IBrandRepository brandRepo = ServiceProvider.GetService<IBrandRepository>();

            await brandRepo.UpdateAsync(Mapper.Map<Brand>(brand));

        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task RemoveAsync(int id)
        {
            IDValidator.AssureID(id);
            IBrandRepository brandRepo = ServiceProvider.GetService<IBrandRepository>();
            
            await brandRepo.RemoveAsync(new Brand { ID = id });

        }

    }
}
