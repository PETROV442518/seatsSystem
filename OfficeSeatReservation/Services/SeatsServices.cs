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

        internal List<Seat> GetAllAvailableSeats()
        {
           return _context.Seats.Where(s => s.IsAvailable).ToList();
        }

        internal List<Seat> GetAllSeats()
        {
            return _context.Seats.ToList();
        }

        internal Seat? GetSeatById(int seatId)
        {
           return _context.Seats.Where(s => s.Id == seatId).FirstOrDefault();
        }

        internal bool IsSeatAvailableForPeriod(int id, DateTime startDate, DateTime endDate)
        {
           var result = false;
           var seat = _context.Reservations.Where(s => s.SeatId == id).FirstOrDefault();
            if (seat != null) {
                if(seat.StartDate <= startDate || seat.EndDate >= endDate) { 
                    result = true;
                }
            }
            return result;
        }

        internal void ReserveSeatForPeriod(int seatId, string employeeName, DateTime startDate, DateTime endDate)
        {
            Seat? seat = GetSeatById(seatId);
            var reservation = new Reservation
            {
                SeatId = seatId,
                EmployeeName = employeeName,
                StartDate = startDate,
                EndDate = endDate
            };

            _context.Reservations.Add(reservation);

            seat.IsAvailable = false;

            _context.SaveChanges();
        }
    }
}
