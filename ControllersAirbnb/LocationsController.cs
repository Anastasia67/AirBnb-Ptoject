using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AirBnb.Services;
using AirBnb.Models;
using AirBnb.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AirBnb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _repository;
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationsController(ILocationRepository repository, ILocationService locationService, IMapper mapper)
        {
            _repository = repository;
            _locationService = locationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations(CancellationToken cancellationToken)
        {
            var locations = await _locationService.GetAllLocationsAsync(cancellationToken);
            var locationDTOs = _mapper.Map<IEnumerable<LocationDto>>(locations);
            return Ok(locationDTOs);
        }

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<LocationDto>>> SearchLocations([FromBody] SearchRequestDto searchRequest, CancellationToken cancellationToken)
        {
            var locations = await _locationService.SearchLocationsAsync(searchRequest, cancellationToken);
            return Ok(locations);
        }

        [HttpGet("GetMaxPrice")]
        public async Task<ActionResult<MaxPriceDto>> GetMaxPrice(CancellationToken cancellationToken)
        {
            var maxPrice = await _locationService.GetMaxPriceAsync(cancellationToken);
            return Ok(new MaxPriceDto { Price = maxPrice });
        }

        [HttpGet("GetDetails/{id}")]
        public async Task<ActionResult<LocationDto>> GetDetails(int id, CancellationToken cancellationToken)
        {
            var locationDetails = await _locationService.GetLocationDetailsAsync(id, cancellationToken);
            return Ok(locationDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id, CancellationToken cancellationToken)
        {
            var location = await _repository.GetById(id, cancellationToken);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location, CancellationToken cancellationToken)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            await _repository.Update(location, cancellationToken);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location, CancellationToken cancellationToken)
        {
            await _repository.Add(location, cancellationToken);
            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id, CancellationToken cancellationToken)
        {
            var location = await _repository.GetById(id, cancellationToken);
            if (location == null)
            {
                return NotFound();
            }

            await _repository.Delete(id, cancellationToken);

            return NoContent();
        }

        [HttpGet("UnAvailableDates/{locationId}")]
        public async Task<ActionResult<UnAvailableDatesResponseDto>> GetUnAvailableDates(int locationId)
        {
            var unavailableDates = await _locationService.GetUnAvailableDatesAsync(locationId);
            return Ok(new UnAvailableDatesResponseDto { UnAvailableDates = unavailableDates });
        }

        private async Task<bool> LocationExists(int id, CancellationToken cancellationToken)
        {
            return await _repository.Exists(id, cancellationToken);
        }
    }
}