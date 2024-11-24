using Faly.Core;

namespace Faly.DataAccessLayer.Interfaces;

using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IAdminRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);
    Task<PaginatedResult<T>> PaginateAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null);
}
