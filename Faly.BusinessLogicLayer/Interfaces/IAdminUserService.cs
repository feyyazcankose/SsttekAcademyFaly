using Faly.Core;
using Faly.Core.Dtos.Admin;
using Faly.DataAccessLayer.Entities;

namespace Faly.BussinessLogicLayer.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAdminUserService
{
    Task<ServiceResult<IEnumerable<AdminUserDto>>> GetAllUsersAsync(); // Tüm kullanıcılar
    Task<ServiceResult<AdminUserDto>> GetUserByIdAsync(int userId); // Kullanıcı detayı
    Task<ServiceResult> CreateUserAsync(CreateUserDto createUserDto); // Yeni kullanıcı oluştur
    Task<ServiceResult> UpdateUserAsync(UpdateUserDto updateUserDto); // Kullanıcı güncelle
    Task<ServiceResult> DeleteUserAsync(int userId); // Kullanıcı sil
}
