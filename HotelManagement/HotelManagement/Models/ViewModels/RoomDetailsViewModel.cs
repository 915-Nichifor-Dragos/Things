namespace HotelManagement.Models.ViewModels;

public class RoomDetailsViewModel
{
    public int Number { get; set; }
    public int People { get; set; }
    public int Price { get; set; }
    public bool AllowsSmoking { get; set; }
    public bool AllowsDogs { get; set; }
    public bool IsReserved { get; set; }
}
