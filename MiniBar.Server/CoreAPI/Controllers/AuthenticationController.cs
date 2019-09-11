using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Core;
using Facade.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedEntities.DTO.Users;

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApiControllerBase
    {
        public AuthenticationController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        //[Authorize]
        //[HttpGet("validate")]
        //public IActionResult ValidateToken()
        //{
        //    return Ok();
        //}


        [HttpPost]
        public async Task<UserDto> Login(LoginDto dto)
        {
            return await this.ServiceProvider.GetService<IAuthenticationManager>().LoginAsync(dto);
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task Logout()
        {
            await this.ServiceProvider.GetService<IAuthenticationManager>().LogoutAsync();
        }
    }
}