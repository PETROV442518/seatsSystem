using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;

public class AvailableSeatsModel : PageModel
{
    private readonly SeatsServices _seatsService;

    public int AvailableSeatsCount { get; set; }
    public IList<Seat> AvailableSeats { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public AvailableSeatsModel(SeatsServices seatsService)
    {
        _seatsService = seatsService;
    }

    public void OnGet()
    {
        // Calculate the count of available seats and store it in AvailableSeatsCount.
        AvailableSeatsCount = _seatsService.GetAvailableSeatsCount();
    }
}
