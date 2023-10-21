using Microsoft.AspNetCore.Identity;

namespace CustomIdentityServer.Models
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
       User FindByIdAsync(int idGuid);
        Task<User> FindByNameAsync(string userName);
    }
}