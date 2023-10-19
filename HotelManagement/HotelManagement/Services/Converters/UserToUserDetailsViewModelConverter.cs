using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class UserToUserDetailsViewModelConverter
{
    public static UserDetailsViewModel ConvertUser(User user, DateTime registrationDate)
    {
        return new UserDetailsViewModel()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            RoleName = user.Role.Name.ToString(),
            Email = user.Email,
            BirthDate = DateOnly.FromDateTime(user.BirthDate),
            RegistrationDate = DateOnly.FromDateTime(registrationDate)
        };
    }
}
