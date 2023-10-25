using OfficeSeatReservation.Domain;

namespace OfficeSeatReservation.Services
{
    public interface ISeatsServices
    {
        IList<Seat> GetAllSeats();
        Seat? GetSeatById(int seatId);
        bool IsSeatAvailableForPeriod(int id, DateTime startDate, DateTime endDate);
        void ReserveSeatForPeriod(int seatId, string employeeName, DateTime startDate, DateTime endDate);
        int GetAvailableSeatsCount();
    }
}
