using AirBnb.Data;
using AirBnb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace AirBnb.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AirBnBDbContext context) : base(context) { }

        public override async Task<IEnumerable<Location>> GetAll(CancellationToken cancellationToken)
        {
            return await Context.Locations.ToListAsync(cancellationToken);
        }

        public override async Task<Location> GetById(int id, CancellationToken cancellationToken)
        {
            return await Context.Locations.FindAsync(new object[] { id }, cancellationToken);
        }

        public override async Task Add(Location location, CancellationToken cancellationToken)
        {
            await Context.Locations.AddAsync(location, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public override async Task Update(Location location, CancellationToken cancellationToken)
        {
            Context.Entry(location).State = EntityState.Modified;
            await Context.SaveChangesAsync(cancellationToken);
        }

        public override async Task Delete(int id, CancellationToken cancellationToken)
        {
            var location = await GetById(id, cancellationToken);
            Context.Locations.Remove(location);
            await Context.SaveChangesAsync(cancellationToken);
        }

        public override async Task<IEnumerable<Location>> Find(Expression<Func<Location, bool>> predicate, CancellationToken cancellationToken)
        {
            return await Context.Locations.Where(predicate).ToListAsync(cancellationToken);
        }

        public override async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            return await Context.Locations.AnyAsync(e => e.Id == id, cancellationToken);
        }
    }
}