using Faly.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace Faly.DataAccessLayer.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser> GetUserByEmailAsync(string email);

    Task<ApplicationUser> GetUserByIdAsync(string userId);

    Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password);

    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
}
