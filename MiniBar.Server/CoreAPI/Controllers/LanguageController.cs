using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Facade.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ApiControllerBase
    {
        public LanguageController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        //[HttpGet("texts/{languageID}")]
        //public async Task<Dictionary<string, string>> GetLanguageTexts(string languageID)
        //{
        //    return await this.ServiceProvider.GetService<ILanguageManager>().GetLanguageTextsAsync(languageID);
        //}

        //[HttpGet("texts/distinct")]
        //public async Task<List<string>> GetAllTextKeys()
        //{
        //    return await this.ServiceProvider.GetService<ILanguageManager>().GetAllTextKeysAsync();
        //}
    }
}