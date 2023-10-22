using OfficeSeatReservation.Domain;

public class Reservation
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public int SeatId { get; set; }
    public Seat Seat { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}