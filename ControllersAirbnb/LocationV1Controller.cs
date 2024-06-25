using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AirBnb.Services;
using AirBnb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirBnb.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LocationsV1Controller : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationsV1Controller(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all locations (v1).
        /// </summary>
        /// <returns>List of locations.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations(CancellationToken cancellationToken)
        {
            var locations = await _locationService.GetAllLocationsAsync(cancellationToken);
            var locationDTOs = _mapper.Map<IEnumerable<LocationDto>>(locations);
            return Ok(locationDTOs);
        }
    }
}