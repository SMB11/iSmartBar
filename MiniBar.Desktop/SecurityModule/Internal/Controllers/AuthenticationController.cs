using Prism.Events;
using SharedEntities.DTO.Users;
using System.Threading.Tasks;
using Security.Internal.Services;
using Security.Internal.Entities;
using Infrastructure.Prism.Events;
using Security.Api;
using System;

namespace Security.Internal.Controllers
{
    class AuthenticationController
    {
        private IAuthenticationService _authService;
        private IEventAggregator _eventAggregator;
        public AuthenticationController(IAuthenticationService authService, IEventAggregator eventAggregator)
        {
            this._authService = authService;
            this._eventAggregator = eventAggregator;
        }

        public async Task AuthenticateAsync(string login, string password)
        {
            UserDto user = null;
            user = await this._authService.AuthenticateAsync(login, password);

            if (user != null)
            {
                AppSecurityContext.SetCurrentPrincipal(new AppPrincipal(new AppIdentity(user.UserName, user.Token)));
                _eventAggregator.GetEvent<AuthenticationStateChanged>().Publish();
            }
        }

        public async void Logout()
        {
            await _authService.LogoutAsync();
            AppSecurityContext.SetCurrentPrincipal(AppPrincipal.Anonymous);

            _eventAggregator.GetEvent<AuthenticationStateChanged>().Publish();
        }
    }
}
