using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSeatReservation.Services;

public class SeatsModel : PageModel
{
    private readonly SeatsServices _seatsServices; 

    public int AvailableSeatsCount { get; set; }

    [BindProperty]
    public DateTime StartDate { get; set; }

    [BindProperty]
    public DateTime EndDate { get; set; }

    public SeatsModel(SeatsServices seatsServices)
    {
        _seatsServices = seatsServices;
    }

    public void OnGet()
    {
        // Initialize the StartDate and EndDate properties as needed.
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(1);
    }

    public IActionResult OnPost()
    {
        // Calculate the count of available seats based on the provided start date and end date.
        AvailableSeatsCount = _seatsServices.GetAvailableSeatsCountForPeriod(StartDate, EndDate);
        return Page();
    }
}
