using baseAPI.Contracts;
using baseAPI.Data;
using baseAPI.Models;
using baseAPI.Utils;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace baseAPI.Services
{
    //middleware class to keep all CRUD logic outside of the endpoints, inheriting from main Interface contract

    public class UserService : IUserService
    {
        private readonly MongoDbContext _context;

        public UserService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users.Find(u => u.Email == id).SingleOrDefaultAsync();
        }

        public async Task<ServiceResult<User>> UpdateAsync(string id, User user)
        {
            var result = new ServiceResult<User>();
            var dbUser = await _context.Users.Find(u => u.Id == id).SingleOrDefaultAsync();
            if (dbUser == null)
            {
                result.Success = false;
                result.Message = "User not found";
            }
            else
            {
                dbUser.Password = HashHelper.HashPassword(user.Password, dbUser.Salt);
                await _context.Users.ReplaceOneAsync(u => u.Id == id, dbUser);
                result.Success = true;
                result.User = dbUser;
            }
            return result;
        }

        public async Task<ServiceResult<User>> DeleteAsync(string id)
        {
            var result = new ServiceResult<User>();
            var deleteResult = await _context.Users.DeleteOneAsync(u => u.Id == id);
            if (deleteResult.DeletedCount == 0)
            {
                result.Success = false;
                result.Message = "User not found";
            }
            else
            {
                result.Success = true;
            }
            return result;
        }

    }
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }
}
