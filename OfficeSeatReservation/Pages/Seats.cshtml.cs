using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;

public class SeatsModel : PageModel
{
    private readonly SeatsServices _seatsServices; // Replace with your data repository class

    public List<Seat> Seats { get; set; }

    public SeatsModel(SeatsServices seatsServices)
    {
        _seatsServices = seatsServices;
    }

    public void OnGet()
    {
        Seats = _seatsServices.GetAllSeats();
    }
}
