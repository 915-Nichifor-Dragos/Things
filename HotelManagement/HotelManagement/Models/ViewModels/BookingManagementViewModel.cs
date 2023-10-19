namespace HotelManagement.Models.ViewModels;

public class BookingManagementViewModel
{
    public Guid Id { get; set; }
    public string Hotel { get; set; }
    public string Room { get; set; }
    public int Days { get; set; }
    public int PricePerNight { get; set; }
    public int TotalPrice { get; set; }
    public DateOnly EndDate { get; set; }
}
