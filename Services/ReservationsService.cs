using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirBnb.Models;
using AirBnb.Repositories;

namespace AirBnb.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<Reservations> _reservationRepository;
        private readonly IRepository<Customers> _customerRepository;

        public ReservationsService(IRepository<Location> locationRepository, IRepository<Reservations> reservationRepository, IRepository<Customers> customerRepository)
        {
            _locationRepository = locationRepository;
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
        }

        public async Task MakeReservationAsync(int locationId, int customerId, DateTime startDate, DateTime endDate)
        {
            var location = await _locationRepository.GetById(locationId);
            if (location == null)
            {
                throw new KeyNotFoundException("Location not found");
            }

            var customer = await _customerRepository.GetById(customerId);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            var reservations = new Reservations
            {
                LocationId = locationId,
                Location = location,
                CustomerId = customerId,
                Customer = customer,
                StartDate = startDate,
                EndDate = endDate
            };

            await _reservationRepository.Add(reservations);
        }

        public async Task CancelReservationAsync(int reservationsId)
        {
            var reservations = await _reservationRepository.GetById(reservationsId);
            if (reservations == null)
            {
                throw new KeyNotFoundException("Reservation not found");
            }

            await _reservationRepository.Delete(reservationsId);
        }
    }
}