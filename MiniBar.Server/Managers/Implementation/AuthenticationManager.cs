﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Facade.Managers;
using Microsoft.AspNetCore.Identity;
using BusinessEntities.Security;
using Microsoft.Extensions.Configuration;
using SharedEntities.DTO.Users;
using Common.Enums;
using Common.ResponseHandling;
using Common.Core;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Managers.Implementation
{
    public class AuthenticationManager : IAuthenticationManager
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationManager(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                List<string> roles = (await _userManager.GetRolesAsync(appUser)).ToList();
                return new UserDto { UserName = appUser.UserName, Token = GenerateJwtToken(appUser, roles).ToString(), Roles = roles};
            }
            throw new ApiException(FaultCode.InvalidUserCredentials);
        }

        
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        [Transaction]
        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var user = new User
            {
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(user, ApplicationUserRole.Client.ToString());
                await _signInManager.SignInAsync(user, false);
                return new UserDto { UserName = user.UserName, Token = GenerateJwtToken(user, (await _userManager.GetRolesAsync(user)).ToList()).ToString() };
            }

            throw new ApiException(FaultCode.InvalidInput);
        }

        private object GenerateJwtToken(User user, List<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };


            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claimsIdentity.Claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
