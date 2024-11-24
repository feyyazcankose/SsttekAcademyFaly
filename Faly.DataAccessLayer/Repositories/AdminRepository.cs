using Faly.Core;
using Faly.DataAccessLayer.Interfaces;

namespace Faly.DataAccessLayer.Repositories;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class AdminRepository<T> : IAdminRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public AdminRepository(DbContext context)
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

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    // Sayfalama metodu
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
