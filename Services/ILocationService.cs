using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirBnb.Models;

namespace AirBnb.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<LocationDto>> GetLocationDetailsAsync(CancellationToken cancellationToken);
        Task<LocationDto> GetLocationDetailsAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<LocationDto>> SearchLocationsAsync(SearchRequestDto searchRequest, CancellationToken cancellationToken);
        Task<decimal> GetMaxPriceAsync(CancellationToken cancellationToken);
        Task<List<DateTime>> GetUnAvailableDatesAsync(int locationId);
    }
}