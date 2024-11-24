using Faly.Core;
using Faly.Core.Dtos.Ecommerce;

namespace Faly.BussinessLogicLayer.Interfaces;

public interface IUserService
{
    Task<ServiceResult<UserDto>> RegisterUserAsync(UserRegistrationDto registrationDto);
    Task<ServiceResult<string>> LoginUserAsync(UserLoginDto loginDto);
}
