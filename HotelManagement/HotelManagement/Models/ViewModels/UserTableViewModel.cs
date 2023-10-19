using HotelManagement.Models.Constants;

namespace HotelManagement.Models.ViewModels;

public class UserTableViewModel
{
    public PaginatedList<UserDetailsViewModel> Users { get; set; }
}
