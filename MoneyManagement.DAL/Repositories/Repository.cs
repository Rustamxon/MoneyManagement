using Microsoft.EntityFrameworkCore;
using MoneyManagement.DAL.Contexts;
using MoneyManagement.DAL.IRepositories;
using MoneyManagement.Domain.Commons;
using System.Linq.Expressions;

namespace MoneyManagement.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext dbContext;
    private readonly DbSet<TEntity> dbSet;
    public Repository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        var existEntity = await this.dbSet.FirstOrDefaultAsync(t => t.Id.Equals(entity.Id));
        if (existEntity is null) return false;
        return true;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
        => (await this.dbSet.AddAsync(entity)).Entity;

    public async Task SaveChangesAsync()
        => await this.dbContext.SaveChangesAsync();

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true)
    {
        IQueryable<TEntity> query = expression is null ? dbSet : dbSet.Where(expression);
        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
        => await this.SelectAll(expression, includes).FirstOrDefaultAsync();

    public async Task<TEntity> UpdateAsync(TEntity entity)
        => (this.dbSet.Update(entity)).Entity;
}
