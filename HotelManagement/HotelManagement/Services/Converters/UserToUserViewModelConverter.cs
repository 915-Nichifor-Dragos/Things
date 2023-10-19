using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class UserToAccountViewModelConverter
{
    public static MyAccountDetailsViewModel ConvertUser(User user)
    {
        if (user == null)
        {
            return null;
        }

        return new MyAccountDetailsViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            BirthDate = DateOnly.FromDateTime(user.BirthDate),
            Gender = user.Gender,
            Role = user.Role,
            Address = user.Address,
            Bio = user.Bio,
            ImageUrl = user.ImageUrl,
        };
    }
}
