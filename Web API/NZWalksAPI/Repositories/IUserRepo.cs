using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Repositories
{
    public interface IUserRepo
    {
        Task<List<User>> GetAllAsync();
        Task<User?> LoginAsync(string email, string password);
        Task<User?> LoginsAsync(string email, string password, User u);
        string CreateTokenAsync(User user);
        Task<User> RegisterUserAsync(User u);
        Task<User> RegisterUserEncryptAsync(User u);
        
    }
}
