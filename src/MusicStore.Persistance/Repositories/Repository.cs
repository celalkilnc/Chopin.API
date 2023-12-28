using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MusicStore.Domain.Entities;

namespace MusicStore.Persistance.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    readonly MSDBContext _context;

    public Repository(MSDBContext context)
    {
        _context = context; 
    }

    public DbSet<T> Table => _context.Set<T>();
    
    public async Task<bool> AddAsync(T model)
    {
        EntityEntry entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> datas)
    {
        await Table.AddRangeAsync(datas);
        return true;
    }

    public IQueryable<T> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.ID == id);
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(method);
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking)
            query = query.AsNoTracking();
        return query;
    }

    public bool Remove(T model)
    {
        EntityEntry entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        T model = await Table.FirstOrDefaultAsync(x => x.ID == id );
        return Remove(model);
    }

    public bool RemoveRange(List<T> datas)
    {
        Table.RemoveRange(datas);
        return true;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();

    public bool Update(T model)
    {
        EntityEntry entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }
}