using Faly.DataAccessLayer.Data;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Faly.DataAccessLayer.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _context
            .Courses.Include(c => c.CourseCategories)
            .ThenInclude(cc => cc.Category)
            .ToListAsync();
    }

    public async Task<Course> GetCourseByIdAsync(int courseId)
    {
        return await _context
            .Courses.Include(c => c.CourseCategories)
            .ThenInclude(cc => cc.Category)
            .Include(c => c.Sections)
            .ThenInclude(s => s.Videos)
            .FirstOrDefaultAsync(c => c.Id == courseId);
    }

    public async Task<IEnumerable<Course>> GetCoursesByNameAsync(string name)
    {
        return await _context
            .Courses.Include(c => c.CourseCategories)
            .ThenInclude(cc => cc.Category)
            .Where(c => c.Name.Contains(name))
            .ToListAsync();
    }
}
