using AirBnb.Data;
using AirBnb.Models;

namespace AirBnb.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(AirBnBDbContext context) : base(context)
        {
        }
    }
}