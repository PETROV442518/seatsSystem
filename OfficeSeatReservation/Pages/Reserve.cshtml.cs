using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using OfficeSeatReservation.Domain;
using OfficeSeatReservation.Services;

public class ReserveModel : PageModel
{
    private readonly SeatsServices  _seatsServices; 
    public int SeatId { get; set; }

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

    public IList<Seat> AvailableSeats { get; set; }
    public SelectList SeatSelectList { get; set; }

    public ReserveModel(SeatsServices seatsServices)
    {
        _seatsServices = seatsServices;
    }

    public void OnGet()
    {

        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(1);
        // Populate the list of available seats for selection.
        AvailableSeats = _seatsServices.GetAvailableSeatsForPeriod(StartDate, EndDate);
        SeatSelectList = new SelectList(AvailableSeats, "Id", "SeatNumber");
    }

    public IActionResult OnPost()
    {
        if (ModelState.IsValid)
        {
            // Check if the selected seat is available for the specified period.
            bool isSeatAvailable = _seatsServices.IsSeatAvailable(SeatId, StartDate, EndDate);

            if (isSeatAvailable)
            {
                // Create the reservation and save it to the database.
                _seatsServices.ReserveSeatForPeriod(SeatId, EmployeeName, StartDate, EndDate);

                // Redirect to a success page after a successful reservation.
                return RedirectToPage("ReservationSuccess");
            }
            else
            {
                // Reservation failed due to seat unavailability. Handle the error.
                ModelState.AddModelError(string.Empty, "The selected seat is no longer available for the specified period.");
            }
        }

        // Re-populate the list of available seats for the dropdown.
        AvailableSeats = _seatsServices.GetAvailableSeatsForPeriod(StartDate, EndDate);
        SeatSelectList = new SelectList(AvailableSeats, "Id", "SeatNumber");

        return Page();
    }
}
