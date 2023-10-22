namespace OfficeSeatReservation.Domain
{
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}
