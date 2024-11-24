using Faly.Core;
using Faly.DataAccessLayer.Interfaces;

namespace Faly.DataAccessLayer.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class ReadOnlyRepository<T> : IReadOnlyRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public ReadOnlyRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<PaginatedResult<T>> PaginateAsync(int pageIndex, int pageSize, Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> orderBy = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }

        int totalCount = await query.CountAsync(); // Toplam kayıt sayısı
        List<T> items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(); // Sayfa verisi

        return new PaginatedResult<T>(items, totalCount, pageIndex, pageSize);
    }
}
