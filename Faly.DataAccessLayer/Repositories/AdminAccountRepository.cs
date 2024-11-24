using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Faly.DataAccessLayer.Repositories;

public class AdminAccountRepository : IAdminAccountRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminAccountRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> ValidateCredentialsAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return false;

        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<string> GetRoleAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return string.Empty;

        var roles = await _userManager.GetRolesAsync(user);
        return roles.Count > 0 ? roles[0] : string.Empty;
    }
}
