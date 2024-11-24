using Faly.Core;
using Faly.Core.Dtos.Admin;
using Faly.DataAccessLayer.Entities;

namespace Faly.BussinessLogicLayer.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAdminCourseService
{
    Task<ServiceResult<IEnumerable<AdminCourseDto>>> GetAllCoursesAsync(); // Tüm kursları listeleme
    Task<ServiceResult<AdminCourseDto>> GetCourseByIdAsync(int courseId); // Kurs detayını görüntüleme
    Task<ServiceResult> AddCourseAsync(CreateCourseDto createCourseDto); // Kurs oluşturma
    Task<ServiceResult> UpdateCourseAsync(UpdateCourseDto updateCourseDto); // Kurs güncelleme
    Task<ServiceResult> DeleteCourseAsync(int courseId); // Kurs silme
}

