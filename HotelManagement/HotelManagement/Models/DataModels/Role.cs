using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.DataModels;

public class Role
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public Constants.Role Name { get; set; }

    [Required]
    public HashSet<User> Users { get; set; } = new HashSet<User>();
}
