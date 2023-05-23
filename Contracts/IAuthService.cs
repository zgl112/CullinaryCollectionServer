using baseAPI.Models;
using System.Threading.Tasks;

namespace baseAPI.Contracts
{
    //Clasic interface to set contracts for what the class should return

    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterAsync(User user);
        Task<AuthenticationResult> LoginAsync(User user);
    }
}
