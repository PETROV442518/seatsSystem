using OfficeSeatReservation.Data;
using OfficeSeatReservation.Domain;

namespace OfficeSeatReservation.Services
{
    public class SeatsServices
    {
        private readonly SeatsReservationContext _context;

        public SeatsServices(SeatsReservationContext context)
        {
            _context = context;
        }

        internal List<Seat> GetAllSeats()
        {

            return _context.Seats.ToList();
        }

        internal Seat? GetSeatById(string seatNumber)
        {
            return _context.Seats.Where(s => s.SeatNumber == seatNumber).FirstOrDefault();
        }

        internal IList<string> getUnavailableSeatsForPeriod(DateTime startDate, DateTime endDate)
        {
            var unavailableSeatsList = new List<string>() { "seat-1", "seat-2", "seat-3", "seat-4", "seat-5", "seat-6" };
            //var unavailableSeatsList = _context.Reservations.Where(r => r.StartDate <= startDate && r.EndDate >= endDate).Select(s => s.SeatNumber).ToList() as IList<string>;
            return unavailableSeatsList;    
        }
    }
}
