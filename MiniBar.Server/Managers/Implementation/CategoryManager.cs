using Facade.Managers;
using SharedEntities.DTO.Product;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Facade.Repository;
using System.Threading.Tasks;
using Common.Core;
using System.Globalization;
using Managers.Base;
using BusinessEntities.Products;
using Common.ResponseHandling;
using Common.Validation;
using SharedEntities.DTO.Global;

namespace Managers.Implementation
{
    public class CategoryManager : ManagerBase, ICategoryManager
    {
        public CategoryManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        
        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            ICategoryRepository catRepo = ServiceProvider.GetService<ICategoryRepository>();
            return await GetDTOs(await catRepo.GetAllAsync());
        }

        public async Task<CategoryDTO> GetByID(int id)
        {
            ICategoryRepository catRepo = ServiceProvider.GetService<ICategoryRepository>();
            return await GetDTO(await catRepo.FindByIDAsync(id));
        }

        public async Task<CategoryUploadDTO> GetForUplaodByID(int id)
        {
            ICategoryRepository categoryRepository = ServiceProvider.GetService<ICategoryRepository>();
            Category category = await categoryRepository.FindByIDAsync(id);

            return await GetCategoryUploadDTO(category);
        }

        private async Task<CategoryUploadDTO> GetCategoryUploadDTO(Category category)
        {

            ICategoryNameRepository categoryNameRepository = ServiceProvider.GetService<ICategoryNameRepository>();
            List<CategoryName> infos = await categoryNameRepository.FindAsync((inf) => inf.CategoryID == category.ID);
            Dictionary<string, string> info = new Dictionary<string, string>();
            foreach (var data in infos)
            {
                info.Add(data.LanguageID,  data.Name );
            }
            return new CategoryUploadDTO()
            {
                ID = category.ID,
                ParentID = category.ParentID,
                Names = info
            };
        }

        public Task<List<CategoryDTO>> GetRootCategoriesAsync()
        {
            return GetSubcategoriesAsync(null);
        }

        public async Task<List<CategoryDTO>> GetSubcategoriesAsync(int? id)
        {
            ICategoryRepository catRepo = ServiceProvider.GetService<ICategoryRepository>();
            List<Category> categories = await catRepo.FindAsync(e => e.ParentID == id);
            List<CategoryDTO> res = await GetDTOs(categories);
            return res;
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task<int> InsertAsync(CategoryUploadDTO category)
        {
            IDValidator.AssureEmpty(category.ID);
            ICategoryRepository catRepo = ServiceProvider.GetService<ICategoryRepository>();
            ICategoryNameRepository catNameRepo = ServiceProvider.GetService<ICategoryNameRepository>();
            Category saved = await catRepo.InsertAsync(new Category() { ParentID = category.ParentID });
            foreach(CultureInfo culture in LocalizationOptions.Value.SupportedCultures)
            {
                if (!category.Names.ContainsKey(culture.Name))
                {
                    throw new ApiException(Common.Enums.FaultCode.NotAllCulturesProvided, culture.EnglishName + " needs to be filled.");
                }
            }
            List<CategoryName> names = new List<CategoryName>();
            foreach (var pair in category.Names)
            {
                await catNameRepo.InsertAsync(new CategoryName()
                {
                    CategoryID = saved.ID,
                    LanguageID = pair.Key,
                    Name = pair.Value
                });
            }
            
            return saved.ID;
        }


        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task InsertChangesAsync(ListChanges<CategoryUploadDTO> changes)
        {
            foreach(var item in changes.NewItems)
            {
                await InsertAsync(item);
            }

            foreach (var item in changes.OldItems)
            {
                await RemoveAsync(item.ID);
            }


            foreach (var item in changes.ChangedItems)
            {
                await UpdateAsync(item);
            }

        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task UpdateAsync(CategoryUploadDTO category)
        {
            IDValidator.AssureID(category.ID);
            ICategoryRepository catRepo = ServiceProvider.GetService<ICategoryRepository>();
            ICategoryNameRepository catNameRepo = ServiceProvider.GetService<ICategoryNameRepository>();
            await catRepo.UpdateAsync(new Category() {
                ID = category.ID,
                ParentID = category.ParentID });

            List<CategoryName> names = new List<CategoryName>();
            foreach (var pair in category.Names)
            {
                await catNameRepo.UpdateAsync(new CategoryName()
                {
                    CategoryID = category.ID,
                    LanguageID = pair.Key,
                    Name = pair.Value
                });
            }
            
        }

        [Transaction(System.Transactions.IsolationLevel.Serializable)]
        public async Task RemoveAsync(int id)
        {
            IDValidator.AssureID(id);
            ICategoryRepository catRepo = ServiceProvider.GetService<ICategoryRepository>();
            ICategoryNameRepository catNameRepo = ServiceProvider.GetService<ICategoryNameRepository>();

            List<CategoryName> names =  await catNameRepo.FindAsync(c => c.CategoryID == id);
            foreach (CategoryName name in names)
            {
                await catNameRepo.RemoveAsync(name);
            }
            await catRepo.RemoveAsync(new Category { ID = id });

        }

        #region Private


        private async Task<CategoryDTO> GetDTO(Category category)
        {
            ICategoryNameRepository catNameRepo = ServiceProvider.GetService<ICategoryNameRepository>();
            return new CategoryDTO()
            {
                ID = category.ID,
                Name = (await catNameRepo.FindByIDAsync(category.ID, Culture.Name)).Name,
                LanguageID = Culture.Name,
                ParentID = category.ParentID
            };
        }

        private async Task<List<CategoryDTO>> GetDTOs(List<Category> categories)
        {
            List<CategoryDTO> res = new List<CategoryDTO>();
            foreach (Category category in categories)
            {
                res.Add(await GetDTO(category));
            }
            return res;
        }

        #endregion

    }
}
