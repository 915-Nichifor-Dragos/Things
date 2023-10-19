namespace HotelManagement.Models.ViewModels;

public class ReservationListingViewModel
{
    public Guid ReservationId { get; set; }

    public string Hotel { get; set; }

    public int Room { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}
