using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MusicStore.API.Domain.Entities;

namespace MusicStore.API.Persistance.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    readonly MusicStoreDbContext _context;

    public Repository(MusicStoreDbContext context)
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

    public async Task<T> GetByIdAsync(int id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());
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

    public async Task<bool> RemoveAsync(int id)
    {
        T model = await Table.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());
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