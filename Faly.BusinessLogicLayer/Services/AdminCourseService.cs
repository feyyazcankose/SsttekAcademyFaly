using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Admin;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;

namespace Faly.BussinessLogicLayer.Services;

public class AdminCourseService : IAdminCourseService
{
   private readonly IAdminRepository<Course> _courseRepository;

    public AdminCourseService(IAdminRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<ServiceResult<IEnumerable<AdminCourseDto>>> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllAsync();
        var courseDtos = courses.Select(course => new AdminCourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Price = course.Price,
            Categories = course.CourseCategories.Select(cc => cc.Category.Name).ToList()
        });

        return ServiceResult<IEnumerable<AdminCourseDto>>.SuccessResult(courseDtos, "Courses retrieved successfully.");
    }

    public async Task<ServiceResult<AdminCourseDto>> GetCourseByIdAsync(int courseId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null)
            return ServiceResult<AdminCourseDto>.ErrorResult("Course not found.");

        var courseDto = new AdminCourseDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Price = course.Price,
            Categories = course.CourseCategories.Select(cc => cc.Category.Name).ToList()
        };

        return ServiceResult<AdminCourseDto>.SuccessResult(courseDto, "Course retrieved successfully.");
    }

    public async Task<ServiceResult> AddCourseAsync(CreateCourseDto createCourseDto)
    {
        var course = new Course
        {
            Name = createCourseDto.Name,
            Description = createCourseDto.Description,
            Price = createCourseDto.Price
        };

        await _courseRepository.AddAsync(course);
        return ServiceResult.SuccessResult(201, "Course created successfully.");
    }

    public async Task<ServiceResult> UpdateCourseAsync(UpdateCourseDto updateCourseDto)
    {
        var course = await _courseRepository.GetByIdAsync(updateCourseDto.Id);
        if (course == null)
            return ServiceResult.ErrorResult("Course not found.");

        course.Name = updateCourseDto.Name;
        course.Description = updateCourseDto.Description;
        course.Price = updateCourseDto.Price;

        await _courseRepository.UpdateAsync(course);
        return ServiceResult.SuccessResult(200, "Course updated successfully.");
    }

    public async Task<ServiceResult> DeleteCourseAsync(int courseId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null)
            return ServiceResult.ErrorResult("Course not found.");

        await _courseRepository.DeleteAsync(course);
        return ServiceResult.SuccessResult(200, "Course deleted successfully.");
    }
}

