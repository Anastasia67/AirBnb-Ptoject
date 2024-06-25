using AirBnb.Data;
using AirBnb.Models;

namespace AirBnb.Repositories
{
    public class LandlordRepository : Repository<Landlord>, ILandlordRepository
    {
        public LandlordRepository(AirBnBDbContext context) : base(context)
        {
        }
    }
}