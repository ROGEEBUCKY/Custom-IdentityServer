using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Claims;

namespace CustomIdentityServer.Models
{
    /// <summary>
    /// This store is only partially implemented. It supports user creation and find methods.
    /// </summary>
    public class CustomUserStore : IUserStore<User>,
        IUserPasswordStore<User>,
        IUserClaimStore<User>
    {
        private readonly IUserRepository _userRepo;

        public CustomUserStore(IUserRepository usersTable)
        {
            _userRepo = usersTable;
        }

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #region createuser
        public async Task<IdentityResult> CreateAsync(User user,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _userRepo.CreateAsync(user);
        }
        #endregion

        public async Task<IdentityResult> DeleteAsync(User user,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _userRepo.DeleteAsync(user);

        }

        public void Dispose()
        {
        }

        public async Task<User> FindByIdAsync(string userId,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userId == null) throw new ArgumentNullException(nameof(userId));
            //Guid idGuid;
            //if (!Guid.TryParse(userId, out idGuid))
            //{
            //    throw new ArgumentException("Not a valid Guid id", nameof(userId));
            //}
            int inte;
            var x = Int32.TryParse(userId, out inte);
            return _userRepo.FindByIdAsync(inte);

        }

        public async Task<User> FindByNameAsync(string userName,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userName == null) throw new ArgumentNullException(nameof(userName));

            return await _userRepo.FindByNameAsync(userName);
        }

        public Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {

            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("Designation", user.Designation.Designation1.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.Role1.ToString()));
            return Task.FromResult(claims); ;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Password);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserID.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Email);
        }

        public Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedName == null) throw new ArgumentNullException(nameof(normalizedName));

            user.Name = normalizedName;
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));

            user.Password = passwordHash;
            return Task.FromResult<object>(null);

        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if(user == null) throw new ArgumentNullException(nameof(user));

            return await _userRepo.CreateAsync(user);
        }
    }
}
