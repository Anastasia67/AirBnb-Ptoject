using System.Collections.Generic;
using System.Threading.Tasks;
using AirBnb.Models;

namespace AirBnb.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<Location>> SearchLocationsAsync(string keyword, int? landlordId);
        Task<IEnumerable<Location>> GetAllLocationsAsync();
    }
}