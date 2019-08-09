using Flurl.Http;
using SharedEntities.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Security.Internal.Services
{

    public interface IAuthenticationService
    {
        Task<UserDto> AuthenticateAsync(string username, string password, CancellationToken token = default(CancellationToken));
        Task LogoutAsync();
    }
    public class AuthenticationService : RestConsumingServiceBase, IAuthenticationService
    {
        public AuthenticationService() : base("authentication")
        {
        }

        [ApiExceptionHandling]
        public async Task<UserDto> AuthenticateAsync(string username, string password, CancellationToken token = default(CancellationToken))
        {
            return await BuildRequest().PostJsonAsync(new LoginDto { Username = username, Password = password}, token).ReceiveJson<UserDto>().ConfigureAwait(false);
        }
        
        [ApiExceptionHandling]
        public async Task LogoutAsync()
        {
            await BuildRequest("logout").SendAsync(System.Net.Http.HttpMethod.Post).ConfigureAwait(false);
        }
    }
}
