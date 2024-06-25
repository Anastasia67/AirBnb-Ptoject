using System.Threading.Tasks;
using AirBnb.Models;

namespace AirBnb.Services
{
    public interface IReservationsService
    {
        Task MakeReservationAsync(int locationId, int customerId, DateTime startDate, DateTime endDate);
        Task CancelReservationAsync(int reservationId);
    }
}