using System.Collections.Generic;
using System.Threading.Tasks;
using AirBnb.Models;
using AirBnb.Repositories;

namespace AirBnb.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<IEnumerable<LocationDetailsDto>> GetLocationDetailsAsync();
    }
}