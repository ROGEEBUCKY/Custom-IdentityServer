using CustomIdentityServer.Data;
using CustomIdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_Server.CustomProvider
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
           await _dbContext.AddAsync<User>(user);
           await _dbContext.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            _dbContext.Remove<User>(user);
            await _dbContext.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public User FindByIdAsync(int idGuid)
        {
            return  _dbContext.User.Include(x => x.Role).Include(x => x.Designation).FirstOrDefault(x => x.UserID == idGuid);
        }

        public async Task<User> FindByNameAsync(string userName)
        {
            return await _dbContext.User.Include(x => x.Role).Include(x => x.Designation).FirstOrDefaultAsync(x => x.Name== userName);
        }
    }
}
