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

        internal async void ReserveSeatForPeriod(int seatId, string employeeName, DateTime startDate, DateTime endDate)
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
        internal int GetAvailableSeatsCount()
        {
            
            return _context.Seats.Where(s => s.IsAvailable == true).Count();
        }

        internal int GetAvailableSeatsCountForPeriod(DateTime startDate, DateTime endDate)
        {
            var allSeatsCount = _context.Seats.Count();
            int reservationsForPeriodCount = _context.Reservations.Where(r => r.StartDate >= startDate && r.EndDate <= endDate).Count();
            return allSeatsCount - reservationsForPeriodCount;
        }
    }
}
