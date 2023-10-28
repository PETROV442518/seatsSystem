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
        private readonly SeatsServices _seatsServices
            ; // Replace with your data repository class

        public SeatController(SeatsServices seatsServices)
        {
            _seatsServices = seatsServices;
        }

        public IActionResult Index()
        {
            // Retrieve the list of available seats from your data repository.
            int availableSeatsCount = _seatsServices.GetAvailableSeatsCount();
            return View(availableSeatsCount);
        }

        public IActionResult CheckAvailability(string seatId, DateTime startDate, DateTime endDate)
        {
            // Check seat availability for a specific period.
            bool isAvailable = _seatsServices.IsSeatAvailableForPeriod(seatId, startDate, endDate);
            return Json(new { available = isAvailable });
        }

        [HttpGet]
        public IActionResult Reserve(int seatId)
        {
            // Show a reservation form with the selected seat information.
            Seat seat = _seatsServices.GetSeatById(seatId);
            return View(seat);
        }

        [HttpPost]
        public IActionResult Reserve(int seatId, string employeeName, DateTime startDate, DateTime endDate)
        {
            // Handle the reservation process.
            var seat = _seatsServices.GetSeatById(seatId);
            _seatsServices.ReserveSeatForPeriod(seatId, employeeName, startDate, endDate);
            return RedirectToAction("Index"); // Redirect to the seat layout after making a reservation.
        }
    }
}

