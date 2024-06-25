using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirBnb.Data;
using AirBnb.Models;
using AirBnb.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AirBnb.Services
{
    public class LocationService : ILocationService
    {
        private readonly AirBnBDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILocationRepository _locationRepository;

        public LocationService(AirBnBDbContext context, IMapper mapper, ILocationRepository locationRepository)
        {
            _context = context;
            _mapper = mapper;
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync(CancellationToken cancellationToken = default)
        {
            return await _locationRepository.GetAll(cancellationToken);
        }

        public async Task<IEnumerable<LocationDto>> GetLocationDetailsAsync(CancellationToken cancellationToken)
        {
            var locations = await _context.Locations
                .Include(l => l.Landlord)
                .ThenInclude(landlord => landlord.Avatar)
                .Include(l => l.Images)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        public async Task<LocationDto> GetLocationDetailsAsync(int id, CancellationToken cancellationToken)
        {
            var location = await _context.Locations
                .Include(l => l.Landlord)
                .ThenInclude(landlord => landlord.Avatar)
                .Include(l => l.Images)
                .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

            return _mapper.Map<LocationDto>(location);
        }

        public async Task<IEnumerable<LocationDto>> SearchLocationsAsync(SearchRequestDto searchRequest, CancellationToken cancellationToken)
        {
            var query = _context.Locations.AsQueryable();

            if (searchRequest.Features.HasValue)
            {
                query = query.Where(l => (int)l.Features == searchRequest.Features.Value);
            }
            if (searchRequest.Type.HasValue)
            {
                query = query.Where(l => (int)l.Type == searchRequest.Type.Value);
            }
            if (searchRequest.MinPrice.HasValue)
            {
                query = query.Where(l => l.PricePerDay >= searchRequest.MinPrice.Value);
            }
            if (searchRequest.MaxPrice.HasValue)
            {
                query = query.Where(l => l.PricePerDay <= searchRequest.MaxPrice.Value);
            }

            var locations = await query.ToListAsync(cancellationToken);
            return _mapper.Map<IEnumerable<LocationDto>>(locations);
        }

        public async Task<decimal> GetMaxPriceAsync(CancellationToken cancellationToken)
        {
            return await _context.Locations.MaxAsync(l => (decimal)l.PricePerDay, cancellationToken);
        }

        public async Task<List<DateTime>> GetUnAvailableDatesAsync(int locationId)
        {
            var unavailableDates = await _context.Reservations
                                                .Where(r => r.LocationId == locationId && r.EndDate >= DateTime.Today)
                                                .Select(r => r.StartDate)
                                                .ToListAsync();
            return unavailableDates;
        }
    }
}