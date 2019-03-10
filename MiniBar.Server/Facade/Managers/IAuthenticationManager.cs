using SharedEntities.DTO.Users;
using System.Threading.Tasks;

namespace Facade.Managers
{
    public interface IAuthenticationManager
    {

        Task<UserDto> LoginAsync(LoginDto model);
        
        Task<UserDto> RegisterAsync(RegisterDto model);
    }
}
