using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirBnb.Models;
using AirBnb.Repositories;

namespace AirBnb.Services
{
    public class SearchService : ISearchService
    {
        private readonly IRepository<Location> _locationRepository;

        public SearchService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<Location>> SearchLocationsAsync(string keyword, int? landlordId)
        {
            var query = _locationRepository.Find(l =>
                (string.IsNullOrEmpty(keyword) || l.Title.Contains(keyword)) &&
                (!landlordId.HasValue || l.LandlordId == landlordId)
            );

            return await query;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _locationRepository.GetAll();
        }
    }
}