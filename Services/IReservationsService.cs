using AirBnb.Models;
using System.Threading.Tasks;

namespace AirBnb.Services
{
    public interface IReservationsService
    {
        Task MakeReservationAsync(CreateReservationRequestDto requestDto);
        Task CancelReservationAsync(int reservationId);
    }
}