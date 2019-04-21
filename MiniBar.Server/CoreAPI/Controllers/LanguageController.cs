using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.Core;
using Facade.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Global;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ApiControllerBase
    {
        public LanguageController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        [HttpGet]
        public async Task<List<LanguageDTO>> GetAll()
        {
            return await this.ServiceProvider.GetService<ILanguageManager>().GetAll();
        }

        //[HttpGet("texts/distinct")]
        //public async Task<List<string>> GetAllTextKeys()
        //{
        //    return await this.ServiceProvider.GetService<ILanguageManager>().GetAllTextKeysAsync();
        //}
    }
}