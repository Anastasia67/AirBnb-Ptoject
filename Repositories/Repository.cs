using AirBnb.Data;
using AirBnb.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AirBnBDbContext Context;

    public Repository(AirBnBDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public virtual async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
    {
        return await Context.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual async Task<T> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task Add(T entity, CancellationToken cancellationToken = default)
    {
        await Context.Set<T>().AddAsync(entity, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task Update(T entity, CancellationToken cancellationToken = default)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task Delete(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetById(id, cancellationToken);
        if (entity != null)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await Context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    public virtual async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<T>().AnyAsync(e => EF.Property<int>(e, "Id") == id, cancellationToken);
    }

    public virtual async Task<T> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
       
        return await Context.Set<T>().FirstOrDefaultAsync(c => EF.Property<string>(c, "Email") == email, cancellationToken);
    }
}