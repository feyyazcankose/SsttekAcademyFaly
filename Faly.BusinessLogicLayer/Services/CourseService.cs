using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Ecommerce;
using Faly.DataAccessLayer.Interfaces;

namespace Faly.BussinessLogicLayer.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<ServiceResult<IEnumerable<CourseDto>>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllCoursesAsync();
        var courseDtos = courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Price = c.Price,
            IsActive = c.IsActive,
            Categories = c.CourseCategories.Select(cc => cc.Category.Name).ToList(),
        });

        return ServiceResult<IEnumerable<CourseDto>>.SuccessResult(
            courseDtos,
            "Courses retrieved successfully."
        );
    }

    public async Task<ServiceResult<CourseDetailDto>> GetCourseByIdAsync(int courseId)
    {
        var course = await _courseRepository.GetCourseByIdAsync(courseId);
        if (course == null)
        {
            return ServiceResult<CourseDetailDto>.ErrorResult("Course not found.");
        }

        var courseDetailDto = new CourseDetailDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Price = course.Price,
            IsActive = course.IsActive,
            Categories = course.CourseCategories.Select(cc => cc.Category.Name).ToList(),
            Sections = course
                .Sections.Select(s => new CourseSectionDto
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    Videos = s
                        .Videos.Select(v => new VideoDto
                        {
                            Id = v.Id,
                            Title = v.Title,
                            Url = v.Url,
                            Description = v.Description,
                            DurationInSeconds = v.DurationInSeconds,
                        })
                        .ToList(),
                })
                .ToList(),
        };

        return ServiceResult<CourseDetailDto>.SuccessResult(
            courseDetailDto,
            "Course details retrieved successfully."
        );
    }

    public async Task<ServiceResult<IEnumerable<CourseDto>>> SearchCoursesByNameAsync(string name)
    {
        var courses = await _courseRepository.GetCoursesByNameAsync(name);
        var courseDtos = courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Price = c.Price,
            IsActive = c.IsActive,
            Categories = c.CourseCategories.Select(cc => cc.Category.Name).ToList(),
        });

        return ServiceResult<IEnumerable<CourseDto>>.SuccessResult(
            courseDtos,
            "Courses retrieved successfully."
        );
    }
}
