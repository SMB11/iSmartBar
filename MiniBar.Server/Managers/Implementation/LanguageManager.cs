using Facade.Managers;
using Facade.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BusinessEntities.Culture;
using Managers.Base;
using Microsoft.Extensions.Caching.Memory;

namespace Managers.Implementation
{
    public class LanguageManager : ManagerBase, ILanguageManager
    {
        public LanguageManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public List<Language> GetAll()
        {
            return MemoryCache.GetOrCreate("languages", (entry) => {
                entry.Priority = CacheItemPriority.High;
                return ServiceProvider.GetService<ILanguageRepository>().GetAll();
            });
        }

        //public async Task<Dictionary<string, string>> GetLanguageTextsAsync(string languageID)
        //{
        //    ILanguageTextRepository repo = this.serviceProvider.GetService<ILanguageTextRepository>();
        //    List<LanguageText> languageTexts = await repo.FindAsync(e => e.LanguageID == languageID);
        //    Dictionary<string, string> dict = new Dictionary<string, string>();
        //    foreach(LanguageText languageText in languageTexts)
        //    {
        //        dict.Add(languageText.TextKey, languageText.Value);
        //    }
        //    return dict;
        //}

        //public async Task<List<string>> GetAllTextKeysAsync()
        //{
        //    return await Task.Run(() => {
        //        ILanguageTextRepository repo = this.serviceProvider.GetService<ILanguageTextRepository>();
        //        return repo.SelectDistinctTexts();
        //    });
        //}
    }
}
