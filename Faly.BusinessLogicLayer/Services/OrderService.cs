using Faly.BussinessLogicLayer.Interfaces;
using Faly.Core;
using Faly.Core.Dtos.Ecommerce;
using Faly.DataAccessLayer.Entities;
using Faly.DataAccessLayer.Interfaces;

namespace Faly.BussinessLogicLayer.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICourseRepository _courseRepository;

    public OrderService(IOrderRepository orderRepository, ICourseRepository courseRepository)
    {
        _orderRepository = orderRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ServiceResult<OrderDto>> CreateOrderAsync(
        string userId,
        CreateOrderDto createOrderDto
    )
    {
        var courses = new List<Course>();
        decimal totalPrice = 0;

        foreach (var courseId in createOrderDto.CourseIds)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            if (course == null)
            {
                return ServiceResult<OrderDto>.ErrorResult($"Course with ID {courseId} not found.");
            }
            courses.Add(course);
            totalPrice += course.Price;
        }

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            TotalPrice = totalPrice,
            OrderDetails = courses
                .Select(c => new OrderDetail
                {
                    CourseId = c.Id,
                    Price = c.Price,
                    Quantity = 1,
                })
                .ToList(),
        };

        await _orderRepository.CreateOrderAsync(order);

        var orderDto = new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalPrice = order.TotalPrice,
            OrderDetails = order
                .OrderDetails.Select(od => new OrderDetailDto
                {
                    CourseId = od.CourseId,
                    CourseName = courses.First(c => c.Id == od.CourseId).Name,
                    Price = od.Price,
                    Quantity = od.Quantity,
                })
                .ToList(),
        };

        return ServiceResult<OrderDto>.SuccessResult(orderDto, "Order created successfully.");
    }

    public async Task<ServiceResult<IEnumerable<OrderDto>>> GetUserOrdersAsync(string userId)
    {
        var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
        var orderDtos = orders.Select(o => new OrderDto
        {
            Id = o.Id,
            OrderDate = o.OrderDate,
            TotalPrice = o.TotalPrice,
            OrderDetails = o
                .OrderDetails.Select(od => new OrderDetailDto
                {
                    CourseId = od.CourseId,
                    CourseName = od.Course.Name,
                    Price = od.Price,
                    Quantity = od.Quantity,
                })
                .ToList(),
        });

        return ServiceResult<IEnumerable<OrderDto>>.SuccessResult(
            orderDtos,
            "Orders retrieved successfully."
        );
    }
}
