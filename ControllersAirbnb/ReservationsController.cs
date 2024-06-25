using Microsoft.AspNetCore.Mvc;
using AirBnb.Services;
using System.Threading.Tasks;
using System;
using AirBnb.Models;

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

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody] CreateReservationRequestDto requestDto)
        {
            await _reservationService.MakeReservationAsync(requestDto);
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