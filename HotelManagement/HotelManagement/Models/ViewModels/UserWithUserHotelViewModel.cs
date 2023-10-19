using HotelManagement.Models.DataModels;

namespace HotelManagement.Models.ViewModels;

public class UserWithUserHotelViewModel
{
    public User User { get; set; }

    public virtual ICollection<UserHotels> UserHotel { get; set; }
}
