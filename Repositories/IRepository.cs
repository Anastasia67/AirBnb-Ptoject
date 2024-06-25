using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AirBnb.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
        Task<T> GetById(int id, CancellationToken cancellationToken = default);
        Task Add(T entity, CancellationToken cancellationToken = default);
        Task Update(T entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<bool> Exists(int id, CancellationToken cancellationToken);
        Task<T> GetByEmail(string email, CancellationToken cancellationToken = default);
    }
}