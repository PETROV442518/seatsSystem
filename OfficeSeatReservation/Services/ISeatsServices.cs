using OfficeSeatReservation.Domain;

namespace OfficeSeatReservation.Services
{
    public interface ISeatsServices
    {
        IList<Seat> GetAllSeats();
        Seat? GetSeatByNumber(string seatNumber);
    }
}
