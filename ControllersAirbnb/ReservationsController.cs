using Microsoft.AspNetCore.Mvc;
using AirBnb.Services;
using System.Threading.Tasks;

namespace AirBnb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationService;

        public ReservationsController(IReservationsService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("{locationId}")]
        public async Task<IActionResult> MakeReservation(int locationId, int customerId, DateTime startDate, DateTime endDate)
        {
            await _reservationService.MakeReservationAsync(locationId, customerId, startDate, endDate);
            return Ok();
        }

        [HttpDelete("{reservationId}")]
        public async Task<IActionResult> CancelReservation(int reservationId)
        {
            await _reservationService.CancelReservationAsync(reservationId);
            return NoContent();
        }
    }
}