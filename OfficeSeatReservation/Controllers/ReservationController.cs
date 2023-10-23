using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeSeatReservation.Data;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;

namespace OfficeSeatReservation.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly SeatsServices _seatsServices;
        public ReservationController(SeatsServices seatsServices)
        {
            _seatsServices = seatsServices;   
        }

    }
}
