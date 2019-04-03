using Facade.Managers;
using Facade.Repository;
using Managers.Base;
using Microsoft.AspNetCore.Http;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BusinessEntities.Products;
using Common.Core;
using System.Globalization;
using Common.ResponseHandling;
using Common.Validation;

namespace Managers.Implementation
{
    public class ProductManager : ManagerBase, IProductManager
    {
        public ProductManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<ProductDTO> GetByID(int id)
        {
            IProductRepository productRepo = ServiceProvider.GetService<IProductRepository>();
            Product prod = await productRepo.FindByIDAsync(id);

            return await GetProductDTO(prod);
        }

        public async Task<ProductUploadDTO> GetForUplaodByID(int id)
        {
            IProductRepository productRepo = ServiceProvider.GetService<IProductRepository>();
            Product prod = await productRepo.FindByIDAsync(id);

            return await GetProductUploadDTO(prod);
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            IProductRepository productRepo = ServiceProvider.GetService<IProductRepository>();
            List<Product> products = await productRepo.GetAllAsync();
            List<ProductDTO> dTOs = new List<ProductDTO>();
            foreach (Product prod in products)
            {
                dTOs.Add(await GetProductDTO(prod));
            }
            return dTOs;
        }

        public async Task<List<ProductDTO>> GetBrandProducts(int id)
        {
            IProductRepository productRepo = ServiceProvider.GetService<IProductRepository>();
            List<Product> products = await productRepo.FindAsync(p => p.BrandID == id);
            List<ProductDTO> dTOs = new List<ProductDTO>();
            foreach(Product prod in products)
            {
                dTOs.Add(await GetProductDTO(prod));
            }
            return dTOs;
        }

        private async Task<ProductDTO> GetProductDTO(Product product)
        {

            IProductInfoRepository productInfoRepo = ServiceProvider.GetService<IProductInfoRepository>();
            ProductInfo info = await productInfoRepo.FindByIDAsync(product.ID, Culture.Name);
            return new ProductDTO()
            {
                ID = product.ID,
                CategoryID = product.CategoryID,
                BrandID = product.BrandID,
                Name = info.Name,
                Description = info.Description,
            };
        }

        private async Task<ProductUploadDTO> GetProductUploadDTO(Product product)
        {

            IProductInfoRepository productInfoRepo = ServiceProvider.GetService<IProductInfoRepository>();
            List<ProductInfo> infos = await productInfoRepo.FindAsync((inf) => inf.ProductID == product.ID);
            Dictionary<string, ProductLangData> info = new Dictionary<string, ProductLangData>();
            foreach(var data in infos)
            {
                info.Add(data.LanguageID, new ProductLangData() { Name = data.Name, Description = data.Description });
            }
            return new ProductUploadDTO()
            {
                ID = product.ID,
                CategoryID = product.CategoryID,
                BrandID = product.BrandID,
                Price = product.Price,
                Info = info
            };
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<int> InsertAsync(ProductUploadDTO product)
        {
            IDValidator.AssureEmpty(product.ID);
            IProductRepository prodRepo = ServiceProvider.GetService<IProductRepository>();
            IProductInfoRepository prodInfoRepo = ServiceProvider.GetService<IProductInfoRepository>();
            Product saved = await prodRepo.InsertAsync(new Product() {  
               BrandID = product.BrandID,
               CategoryID = product.CategoryID,
               Price = product.Price
            });
            foreach (CultureInfo culture in LocalizationOptions.Value.SupportedCultures)
            {
                if (!product.Info.ContainsKey(culture.Name))
                {
                    throw new ApiException(Common.Enums.FaultCode.NotAllCulturesProvided, culture.EnglishName + " needs to be filled.");
                }
            }
            foreach (var pair in product.Info)
            {
                await prodInfoRepo.InsertAsync(new ProductInfo()
                {
                    ProductID = saved.ID,
                    LanguageID = pair.Key,
                    Name = pair.Value.Name,
                    Description = pair.Value.Description
                });
            }

            return saved.ID;
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task UpdateAsync(ProductUploadDTO product)
        {
            IDValidator.AssureID(product.ID);
            IProductRepository prodRepo = ServiceProvider.GetService<IProductRepository>();
            IProductInfoRepository prodInfoRepo = ServiceProvider.GetService<IProductInfoRepository>();
            await prodRepo.UpdateAsync(new Product()
            {
                ID = product.ID,
                BrandID = product.BrandID,
                CategoryID = product.CategoryID,
                Price = product.Price
            });
            if (product.Info != null)
            {
                foreach (var pair in product.Info)
                {
                    await prodInfoRepo.UpdateAsync(new ProductInfo()
                    {
                        ProductID = product.ID,
                        LanguageID = pair.Key,
                        Name = pair.Value.Name,
                        Description = pair.Value.Description
                    });
                }
            }

        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task RemoveAsync(int id)
        {
            IDValidator.AssureID(id);
            IProductRepository prodRepo = ServiceProvider.GetService<IProductRepository>();
            IProductInfoRepository prodInfoRepo = ServiceProvider.GetService<IProductInfoRepository>();

            List<ProductInfo> infos = await prodInfoRepo.FindAsync(c => c.ProductID == id);
            foreach (ProductInfo info in infos)
            {
                await prodInfoRepo.RemoveAsync(info);
            }
            await prodRepo.RemoveAsync(new Product { ID = id });

        }
    }
}
