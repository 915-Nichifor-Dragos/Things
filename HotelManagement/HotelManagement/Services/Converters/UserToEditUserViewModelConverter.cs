using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class UserToEditUserViewModelConverter
{
    public static EditUserViewModel ConvertUser(User user, List<Role> roles)
    {
        return new EditUserViewModel()
        {
            Username = user.Username,
            Role = user.RoleId,
            AvailableRoles = roles.Select(role => new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name.ToString(),
            }).ToList()
        };
    }
}
