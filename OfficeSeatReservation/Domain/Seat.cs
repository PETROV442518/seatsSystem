using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeSeatReservation.Domain
{
    [Table("Seats")]
    public class Seat
    {
        public int Id { get; set; }
        public string SeatNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}
