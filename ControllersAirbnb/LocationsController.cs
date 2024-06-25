using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repositories;
using AirBnb.Services;
using AutoMapper;

namespace AirBnb.ControllersAirbnb
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _repository;
        private readonly ISearchService _searchService;
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationsController(ILocationRepository repository, ISearchService searchService, ILocationService locationService, IMapper mapper)
        {
            _repository = repository;
            _searchService = searchService;
            _locationService = locationService;
            _mapper = mapper;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDetailsDto>>> GetLocations()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            var locationDTOs = _mapper.Map<IEnumerable<LocationDetailsDto>>(locations);
            return Ok(locationDTOs);
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDetailsDto>>> GetLocationDetails()
        {
            var locationDetails = await _locationService.GetLocationDetailsAsync();
            return Ok(locationDetails);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _repository.GetById(id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            await _repository.Update(location);

            return NoContent();
        }

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
            await _repository.Add(location);
            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _repository.GetById(id);
            if (location == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return NoContent();
        }

        private async Task<bool> LocationExists(int id)
        {
            return await _repository.Exists(id);
        }

        // GET: api/Locations/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Location>>> GetAll()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(locations);
        }

        // Search locations by keyword and landlordId
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Location>>> SearchLocations(string keyword, int? landlordId)
        {
            var locations = await _searchService.SearchLocationsAsync(keyword, landlordId);
            return Ok(locations);
        }
    }
}
