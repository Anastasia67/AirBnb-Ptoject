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

namespace AirBnb.ControllersAirbnb
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandlordsController : ControllerBase
    {
        private readonly ILandlordRepository _repository;
        private readonly ISearchService _searchService;

        public LandlordsController(ILandlordRepository repository, ISearchService searchService)
        {
            _repository = repository;
            _searchService = searchService;
            _searchService = searchService;
        }


        // GET: api/Landlords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Landlord>>> GetLandlords()
        {
            var landlords = await _repository.GetAll();
            return Ok(landlords);
        }

        // GET: api/Landlords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Landlord>> GetLandlord(int id)
        {
            var landlord = await _repository.GetById(id);

            if (landlord == null)
            {
                return NotFound();
            }

            return Ok(landlord);
        }

        // PUT: api/Landlords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLandlord(int id, Landlord landlord)
        {
            if (id != landlord.Id)
            {
                return BadRequest();
            }

            await _repository.Update(landlord);

            return NoContent();
        }

        // POST: api/Landlords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Landlord>> PostLandlord(Landlord landlord)
        {
            await _repository.Add(landlord);
            return CreatedAtAction(nameof(GetLandlord), new { id = landlord.Id }, landlord);
        }

        // DELETE: api/Landlords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLandlord(int id)
        {
            var landlord = await _repository.GetById(id);
            if (landlord == null)
            {
                return NotFound();
            }

            await _repository.Delete(id);

            return NoContent();
        }

        // GET: api/Landlords/locations
        [HttpGet("locations")]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
        {
            var locations = await _searchService.GetAllLocationsAsync();
            return Ok(locations);
        }

        private async Task<bool> LandlordExists(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
