using OfficeSeatReservation.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

[Table("Reservations")]
public class Reservation
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string SeatNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}