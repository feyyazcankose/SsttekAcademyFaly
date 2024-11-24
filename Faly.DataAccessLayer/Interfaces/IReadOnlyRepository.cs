using Faly.Core;

namespace Faly.DataAccessLayer.Interfaces;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

public interface IReadOnlyRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);

    // Sayfalama metodu
    Task<PaginatedResult<T>> PaginateAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null);
}
