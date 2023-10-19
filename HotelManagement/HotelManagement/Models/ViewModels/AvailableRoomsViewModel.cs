using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Models.ViewModels;

public class AvailableRoomsViewModel
{
    public Guid HotelId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
