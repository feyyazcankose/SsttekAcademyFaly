namespace Faly.DataAccessLayer.Interfaces;

public interface IAdminAccountRepository
{
    Task<bool> ValidateCredentialsAsync(string email, string password);
    Task<string> GetRoleAsync(string email);
}
