using AirBnb.Models;
using AirBnb.Repositories;
using System;
using System.Threading.Tasks;

namespace AirBnb.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IRepository<Customers> _customerRepository;

        public ReservationsService(ILocationRepository locationRepository, IReservationRepository reservationRepository, IRepository<Customers> customerRepository)
        {
            _locationRepository = locationRepository;
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
        }

        public async Task MakeReservationAsync(CreateReservationRequestDto requestDto)
        {
            var location = await _locationRepository.GetById(requestDto.LocationId, default);
            if (location == null)
            {
                throw new KeyNotFoundException("Location not found");
            }

            var customer = await _customerRepository.GetByEmail(requestDto.Email);
            if (customer == null)
            {
                customer = new Customers
                {
                    Email = requestDto.Email,
                    FirstName = requestDto.FirstName,
                    LastName = requestDto.LastName
                };
                await _customerRepository.Add(customer, default);
            }

            var reservation = new Reservations
            {
                LocationId = requestDto.LocationId,
                CustomerId = customer.Id,
                StartDate = requestDto.StartDate,
                EndDate = requestDto.EndDate,
                Discount = requestDto.Discount ?? 0 
            };

            await _reservationRepository.Add(reservation, default);
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            var reservation = await _reservationRepository.GetById(reservationId, default);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found");
            }

            await _reservationRepository.Delete(reservationId, default);
        }
    }
}