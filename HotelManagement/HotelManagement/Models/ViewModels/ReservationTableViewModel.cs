namespace HotelManagement.Models.ViewModels;

public class ReservationTableViewModel
{
    public PaginatedList<ReservationListingViewModel> Reservations { get; set; }
}
