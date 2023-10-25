using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;

public class SeatsModel : PageModel
{
    private readonly SeatsServices _seatsService;
    public int AvailableSeatsCount { get; set; }

    public List<Seat> Seats { get; set; }

    public SeatsModel(SeatsServices seatsService)
    {
        _seatsService = seatsService;
    }

    public void OnGet()
    {
        // Calculate the count of available seats and store it in AvailableSeatsCount.
        AvailableSeatsCount = _seatsService.GetAvailableSeatsCount();
    }
}
