using System.Collections.Generic;
using System.Threading.Tasks;
using AirBnb.Models;
using AirBnb.Repositories;
using AutoMapper;

namespace AirBnb.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _locationRepository.GetAll();
        }

        public async Task<IEnumerable<LocationDetailsDto>> GetLocationDetailsAsync()
        {
            var locations = await _locationRepository.GetAll();
            return _mapper.Map<IEnumerable<LocationDetailsDto>>(locations);
        }
    }
}