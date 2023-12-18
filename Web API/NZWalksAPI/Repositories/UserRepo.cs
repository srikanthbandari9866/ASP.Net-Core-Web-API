using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalksAPI.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IConfiguration configuration;
        public UserRepo(NZWalksDbContext dbContext, IConfiguration configuration)
        { 
            _dbContext = dbContext;
            this.configuration = configuration;
        }

        public  string CreateTokenAsync(User user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name,user.UserName));
            claims.Add(new Claim(ClaimTypes.Email,user.Email));
            claims.Add(new Claim(ClaimTypes.Role,user.Role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:ValidAudience"],
                configuration["Jwt:ValidIssuer"],
                claims,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email==email && x.Password==password);
        }
        public async Task<User?> LoginsAsync(string email, string password, User u)
        {
           var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email==email);
            }
            return null;
        }

        public async Task<User> RegisterUserAsync(User u)
        {
            await _dbContext.Users.AddAsync(u);
            await _dbContext.SaveChangesAsync();

            return u;
        }
        public async Task<User> RegisterUserEncryptAsync(User u)
        {
            var user = new User
            {
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role,
                Password = BCrypt.Net.BCrypt.HashPassword(u.Password)
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}
