using Faly.DataAccessLayer.Entities;

namespace Faly.DataAccessLayer.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllCoursesAsync();
    Task<Course> GetCourseByIdAsync(int courseId);
    Task<IEnumerable<Course>> GetCoursesByNameAsync(string name);
}
