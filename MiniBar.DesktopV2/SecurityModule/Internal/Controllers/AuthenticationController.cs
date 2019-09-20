using Infrastructure.Security;
using Prism.Events;
using Security.Internal.Services;
using SharedEntities.DTO.Users;
using System.Threading;
using System.Threading.Tasks;

namespace Security.Internal.Controllers
{
    public class AuthenticationController
    {
        private IAuthenticationService _authService;
        private IEventAggregator _eventAggregator;
        public AuthenticationController(IAuthenticationService authService, IEventAggregator eventAggregator)
        {
            this._authService = authService;
            this._eventAggregator = eventAggregator;
        }

        public async Task AuthenticateAsync(string login, string password, CancellationToken token = default(CancellationToken))
        {
            UserDto user = null;
            user = await this._authService.AuthenticateAsync(login, password, token);

            if (user != null)
            {
                AppSecurityContext.SetCurrentPrincipal(new AppPrincipal(new AppIdentity(user.UserName, user.Token)));
            }
        }

        public async Task Logout()
        {
            await _authService.LogoutAsync();
            AppSecurityContext.SetCurrentPrincipal(AppPrincipal.Anonymous);
        }
    }
}
