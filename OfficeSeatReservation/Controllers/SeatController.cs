using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeSeatReservation.Data;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;

namespace OfficeSeatReservation.Controllers
{


    [Route("api/seats")]
    [ApiController]
    public class SeatController : Controller
    {
        private readonly SeatsServices _seatsServices;

        public SeatController(SeatsServices seatsServices)
        {
            _seatsServices = seatsServices;
        }

        [HttpGet]
        [Route("/api/UnavailableSeats")]
        public IActionResult GetUnavailableSeats([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                IList<string> unavailableSeats = new List<string>();
                unavailableSeats = _seatsServices.getUnavailableSeatsForPeriod(startDate, endDate);
                return new JsonResult(new { unavailableSeats });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately.
                return StatusCode(500, "Internal Server Error");
            }
        }

        public IActionResult Index()
        {   
            return View();
        }

        [HttpGet]
        public IActionResult Reserve(string seatNumber)
        {
            // Show a reservation form with the selected seat information.
            Seat seat = _seatsServices.GetSeatById(seatNumber);
            return View(seat);
        }

        [HttpPost]
        public IActionResult Reserve(string seatNumber, string employeeName, DateTime startDate, DateTime endDate)
        {
            // Handle the reservation process.
            var seat = _seatsServices.GetSeatById(seatNumber);
            return RedirectToAction("Index"); // Redirect to the seat layout after making a reservation.
        }
    }
}

