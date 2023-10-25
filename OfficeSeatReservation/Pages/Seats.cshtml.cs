using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;

public class SeatsModel : PageModel
{
    private readonly SeatsServices _seatsService;
    public int AvailableSeatsCount { get; set; }

    public List<Seat> Seats { get; set; }
    [BindProperty]
    public string EmployeeName { get; set; }
    [BindProperty]
    public DateTime StartDate { get; set; }
    [BindProperty]
    public DateTime EndDate { get; set; }
    public Seat Seat { get; set; }

    public SeatsModel(SeatsServices seatsService)
    {
        _seatsService = seatsService;
    }

    public void OnGet()
    {
        // Calculate the count of available seats and store it in AvailableSeatsCount.
        AvailableSeatsCount = _seatsService.GetAvailableSeatsCount();
    }

    public IActionResult OnPost(int seatId)
    {
        Seat = _seatsService.GetSeatById(seatId);

        if (Seat == null || !Seat.IsAvailable)
        {
            TempData["Message"] = "Seat not found or already reserved.";
            return RedirectToPage("./Seats");
        }

        if (ModelState.IsValid)
        {
            if (StartDate < DateTime.Today || EndDate < StartDate)
            {
                ModelState.AddModelError(string.Empty, "Invalid date range.");
                return Page();
            }

            // Handle the reservation logic here, including checking availability for the specified period.
            bool isAvailable = _seatsService.IsSeatAvailableForPeriod(Seat.Id, StartDate, EndDate);

            if (isAvailable)
            {
                // Create a reservation for the specified period
                _seatsService.ReserveSeatForPeriod(Seat.Id, EmployeeName, StartDate, EndDate);

                TempData["Message"] = "Reservation successful.";
                return RedirectToPage("./Seats");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Seat not available for the specified period.");
            }
        }

        return Page();
    }
}
