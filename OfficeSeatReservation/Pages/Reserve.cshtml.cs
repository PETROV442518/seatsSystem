using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;

public class ReserveModel : PageModel
{
    private readonly SeatsServices _seatService; // Replace with your data repository class

    [BindProperty]
    public string EmployeeName { get; set; }
    [BindProperty]
    public DateTime StartDate { get; set; }
    [BindProperty]
    public DateTime EndDate { get; set; }
    public Seat Seat { get; set; }

    public ReserveModel(SeatsServices seatsServices)
    {
        _seatService = seatsServices;
    }

    public IActionResult OnGet(int seatId)
    {
        Seat = _seatService.GetSeatById(seatId);

        if (Seat == null || !Seat.IsAvailable)
        {
            TempData["Message"] = "Seat not found or already reserved.";
            return RedirectToPage("./Seats");
        }

        return Page();
    }

    public IActionResult OnPost(int seatId)
    {
        Seat = _seatService.GetSeatById(seatId);

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
            bool isAvailable = _seatService.IsSeatAvailableForPeriod(Seat.Id, StartDate, EndDate);

            if (isAvailable)
            {
                // Create a reservation for the specified period
                _seatService.ReserveSeatForPeriod(Seat.Id, EmployeeName, StartDate, EndDate);

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
