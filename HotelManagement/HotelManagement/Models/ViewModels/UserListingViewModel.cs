using HotelManagement.Models.DataModels;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.ViewModels;

public class UserListingViewModel
{
    [Required]
    public List<Hotel> Hotels { get; set; }
}
