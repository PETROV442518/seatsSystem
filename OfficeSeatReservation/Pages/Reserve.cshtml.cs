using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;

public class ReserveModel : PageModel
{
    private readonly SeatsServices _seatsServices;

    public Seat SelectedSeat { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Please enter your name.")]
    public string EmployeeName { get; set; }

    [BindProperty]
    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime StartDate { get; set; }

    [BindProperty]
    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime EndDate { get; set; }

    public ReserveModel(SeatsServices seatsServices)
    {
       _seatsServices = seatsServices;
    }

    public IActionResult OnGet(int seatId)
    {
        // Retrieve the selected seat based on seatId.
        if (SelectedSeat == null)
        {
            return RedirectToPage("CheckAvailableSeats"); // Redirect if the seat doesn't exist.
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            int seatID = _seatsServices.GetAllSeats().FirstOrDefault().Id;
            _seatsServices.ReserveSeatForPeriod(seatID, EmployeeName, StartDate, EndDate);
                // Reservation successful, you can redirect to a success page.
               return RedirectToPage("ReservationSuccess");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Reservation failed. The seat may no longer be available for the specified period.");
        }
        return Page();
    }
}
