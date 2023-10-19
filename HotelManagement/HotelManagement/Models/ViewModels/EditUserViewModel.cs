using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.ViewModels;

public class EditUserViewModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public int Role { get; set; }

    public List<RoleViewModel> AvailableRoles { get; set; }
}
