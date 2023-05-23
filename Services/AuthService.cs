using baseAPI.Contracts;
using baseAPI.Data;
using baseAPI.Models;
using baseAPI.Utils;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace baseAPI.Services
{
    //middleware class to keep all CRUD logic outside of the endpoints, inheriting from main Interface
    public class AuthService : IAuthService
    {
        private readonly MongoDbContext _context;

        public AuthService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<AuthenticationResult> RegisterAsync(User user)
        {
            if (await _context.Users.Find(u => u.Email == user.Email).AnyAsync())
            {
                return new AuthenticationResult { Success = false, ErrorMessage = "Email already registered" };
            }
            user.Salt = HashHelper.GenerateSalt();
            user.Password = HashHelper.HashPassword(user.Password, user.Salt);
            await _context.Users.InsertOneAsync(user);
            return new AuthenticationResult { Success = true, User = user };
        }

        public async Task<AuthenticationResult> LoginAsync(User user)
        {
            var dbUser = await _context.Users.Find(u => u.Email == user.Email).SingleOrDefaultAsync();
            if (dbUser == null)
            {
                return new AuthenticationResult { Success = false, ErrorMessage = "Email is not registered" };
            }
            if (!HashHelper.VerifyPassword(user.Password, dbUser.Salt, dbUser.Password))
            {
                return new AuthenticationResult { Success = false, ErrorMessage = "Password is incorrect" };
            }
            return new AuthenticationResult { Success = true, User = dbUser };
        }
    }
}
