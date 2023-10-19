namespace HotelManagement.Models.ViewModels;

public class BookingTableViewModel
{
    public PaginatedList<BookingManagementViewModel> Bookings { get; set; }
}
