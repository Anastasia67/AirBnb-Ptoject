using AirBnb.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirBnb.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IEnumerable<Location>> GetAll(CancellationToken cancellationToken);
        Task<Location> GetById(int id, CancellationToken cancellationToken);
        Task Add(Location location, CancellationToken cancellationToken);
        Task Update(Location location, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Location>> Find(System.Linq.Expressions.Expression<System.Func<Location, bool>> predicate, CancellationToken cancellationToken);
        Task<bool> Exists(int id, CancellationToken cancellationToken);
    }
}