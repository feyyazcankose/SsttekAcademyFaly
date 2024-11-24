using Faly.Core;
using Faly.Core.Dtos.Admin;
using Faly.DataAccessLayer.Entities;

namespace Faly.BussinessLogicLayer.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAdminUserOrderCourseService
{
    Task<ServiceResult<IEnumerable<AdminCourseDto>>> GetCoursesByOrderAsync(int orderId); // Siparişe ait kursları getir

}
