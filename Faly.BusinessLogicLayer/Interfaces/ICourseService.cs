using Faly.Core;
using Faly.Core.Dtos.Ecommerce;

namespace Faly.BussinessLogicLayer.Interfaces;

public interface ICourseService
{
    Task<ServiceResult<IEnumerable<CourseDto>>> GetAllCoursesAsync();
    Task<ServiceResult<CourseDetailDto>> GetCourseByIdAsync(int courseId);
    Task<ServiceResult<IEnumerable<CourseDto>>> SearchCoursesByNameAsync(string name);
}
