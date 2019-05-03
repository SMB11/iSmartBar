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
using Common.Enums;
using SharedEntities.Enum;

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
            Product prod = await productRepo.LoadWith(p => p.Image).LoadWith(p => p.Category).LoadWith(p => p.Brand).FindByIDAsync(id);

            return await GetProductDTO(prod);
        }

        public async Task<ProductUploadDTO> GetForUplaodByID(int id)
        {
            IProductRepository productRepo = ServiceProvider.GetService<IProductRepository>();
            Product prod = await productRepo.LoadWith(p => p.Image).FindByIDAsync(id);

            return await GetProductUploadDTO(prod);
        }

        public async Task<List<ProductDTO>> GetAll()
        {
            IProductRepository productRepo = ServiceProvider.GetService<IProductRepository>();
            List<Product> products = await productRepo.LoadWith(r => r.Brand).GetAllAsync();
            List<ProductDTO> dTOs = new List<ProductDTO>();
            foreach (Product prod in products)
            {
                dTOs.Add(await GetProductDTO(prod));
            }
            return dTOs;
        }

        public async Task<List<ProductDTO>> GetTopFive()
        {
            IProductRepository productRepo = ServiceProvider.GetService<IProductRepository>();
            List<Product> products = await productRepo.Limit(5).LoadWith(r => r.Brand).LoadWith(r => r.Category).LoadWith(r => r.Image).GetAllAsync();
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
            List<Product> products = await productRepo.LoadWith(p => p.Image).FindAsync(p => p.BrandID == id);
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
            ICategoryNameRepository categoryNameRepo = ServiceProvider.GetService<ICategoryNameRepository>();
            ProductInfo info = await productInfoRepo.FindByIDAsync(product.ID, Culture.Name);
            CategoryName catName = await categoryNameRepo.FindByIDAsync(product.CategoryID, Culture.Name);
            return new ProductDTO()
            {
                ID = product.ID,
                Category = catName.Name,
                CategoryID = product.CategoryID,
                BrandID = product.BrandID,
                Brand = product.Brand?.Name,
                Name = info.Name,
                Price = product.Price,
                Size = (int)product.Size,
                Description = info.Description,
                ImagePath = product.Image?.Path
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
                Info = info,
                Size = (ProductSize)product.Size,
                ImagePath = product.Image?.Path
            };
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<int> InsertAsync(ProductUploadDTO product)
        {
            AssureGoodProduct(product);

            IProductRepository prodRepo = ServiceProvider.GetService<IProductRepository>();
            IImageManager imageManager = ServiceProvider.GetService<IImageManager>();
            IProductInfoRepository prodInfoRepo = ServiceProvider.GetService<IProductInfoRepository>();
            Product toInsert = new Product()
            {
                BrandID = product.BrandID,
                CategoryID = product.CategoryID,
                Price = product.Price,
                Size = (BusinessEntities.Enums.ProductSize)product.Size
            };
            if(product.Image != null)
                toInsert.ImageID = (await imageManager.InsertBytesAsync(product.Image)).ID;
            Product saved = await prodRepo.InsertAsync(toInsert);
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
            AssureGoodProduct(product);
            IProductRepository prodRepo = ServiceProvider.GetService<IProductRepository>();
            IImageManager imageManager = ServiceProvider.GetService<IImageManager>();
            IProductInfoRepository prodInfoRepo = ServiceProvider.GetService<IProductInfoRepository>();

            Product old = (await prodRepo
                .LoadWith(p => p.Image)
                .FindByIDAsync(product.ID));

            if (old == null)
                throw new ApiException(FaultCode.InvalidID);
            Product toUpd = new Product()
            {
                ID = product.ID,
                BrandID = product.BrandID,
                CategoryID = product.CategoryID,
                Price = product.Price,
                ImageID = old.ImageID,
                Size = (BusinessEntities.Enums.ProductSize)product.Size
            };

            if (product.Image != null) {
                if(old.Image != null)
                    toUpd.ImageID = (await imageManager.UpdateBytesAsync(product.Image, old.Image.Path)).ID;
                else
                    toUpd.ImageID = (await imageManager.InsertBytesAsync(product.Image)).ID;
            }

            await prodRepo.UpdateAsync(toUpd);
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
            IImageManager imageManager = ServiceProvider.GetService<IImageManager>();
            IProductInfoRepository prodInfoRepo = ServiceProvider.GetService<IProductInfoRepository>();

            Product old = (await prodRepo
                .LoadWith(p => p.Image)
                .FindByIDAsync(id));

            if (old == null)
                throw new ApiException(FaultCode.InvalidID);

            List<ProductInfo> infos = await prodInfoRepo.FindAsync(c => c.ProductID == id);
            foreach (ProductInfo info in infos)
            {
                await prodInfoRepo.RemoveAsync(info);
            }
            await prodRepo.RemoveAsync(new Product { ID = id });
            await imageManager.RemoveAsync(old.Image);


        }

        #region Private Validation Logic

        private void AssureGoodProduct(ProductUploadDTO obj)
        {
            IDValidator.AssureID(obj.BrandID);
            IDValidator.AssureID(obj.CategoryID);
            //ImageValidator.AssureNonEmpty(obj.Image);
        }

        #endregion
    }
}
