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
            _context.seats
        }

        internal List<Seat> GetAllSeats()
        {
            throw new NotImplementedException();
        }

        internal Seat GetSeatById(int seatId)
        {
            throw new NotImplementedException();
        }

        internal bool IsSeatAvailableForPeriod(int id, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        internal void ReserveSeatForPeriod(int seatId, string employeeName, DateTime startDate, DateTime endDate)
        {
            var seat = GetSeatById(seatId);
            var reservation = new Reservation
            {
                SeatId = seatId,
                EmployeeName = employeeName,
                StartDate = startDate,
                EndDate = endDate
            };

            // Add the reservation to the database context and save changes.
            _context.Reservations.Add(reservation);

            // Update the seat's availability status.
            seat.IsAvailable = false;

            // Save changes to the database.
            _context.SaveChanges();
        }
    }
}
