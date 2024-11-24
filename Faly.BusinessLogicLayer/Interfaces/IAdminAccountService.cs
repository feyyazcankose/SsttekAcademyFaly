using Faly.Core;
using Faly.Core.Dtos.Admin;

namespace Faly.BussinessLogicLayer.Interfaces;

public interface IAdminAccountService
{
    Task<ServiceResult<LoginResponseDto>> LoginAsync(AdminLoginDto loginDto);
}
