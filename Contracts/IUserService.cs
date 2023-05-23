using baseAPI.Models;
using baseAPI.Services;
using System.Threading.Tasks;

namespace baseAPI.Contracts
{
    //Clasic interface to set contracts for what the inheriting class should return

    public interface IUserService
    {
        Task<User> GetByIdAsync(string id);
        Task<ServiceResult<User>> UpdateAsync(string id, User user);
        Task<ServiceResult<User>> DeleteAsync(string id);
    }
}
