using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Admin;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;

namespace Faly.BussinessLogicLayer.Services;

public class AdminUserOrderCourseService : IAdminUserOrderCourseService
{
    private readonly IAdminRepository<OrderDetail> _orderDetailRepository;
    private readonly IAdminRepository<Course> _courseRepository;

    public AdminUserOrderCourseService(
        IAdminRepository<OrderDetail> orderDetailRepository,
        IAdminRepository<Course> courseRepository)
    {
        _orderDetailRepository = orderDetailRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ServiceResult<IEnumerable<AdminCourseDto>>> GetCoursesByOrderAsync(int orderId)
    {
        var orderDetails = await _orderDetailRepository.GetByConditionAsync(od => od.OrderId == orderId);

        if (!orderDetails.Any())
            return ServiceResult<IEnumerable<AdminCourseDto>>.ErrorResult("No courses found for this order.");

        var courseIds = orderDetails.Select(od => od.CourseId).Distinct();
        var courses = await _courseRepository.GetByConditionAsync(c => courseIds.Contains(c.Id));

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
}
