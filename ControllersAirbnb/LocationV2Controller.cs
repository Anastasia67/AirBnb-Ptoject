using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AirBnb.Services;
using AirBnb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirBnb.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LocationsV2Controller : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationsV2Controller(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all locations (v2).
        /// </summary>
        /// <returns>List of locations with additional details.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDtoV2>>> GetLocations(CancellationToken cancellationToken)
        {
            var locations = await _locationService.GetAllLocationsAsync(cancellationToken);
            var locationDTOs = _mapper.Map<IEnumerable<LocationDtoV2>>(locations);
            return Ok(locationDTOs);
        }
    }
}