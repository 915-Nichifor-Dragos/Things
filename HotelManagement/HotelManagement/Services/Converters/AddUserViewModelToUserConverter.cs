using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public class AddUserViewModelToUserConverter
{
    public static User ConvertedUser(AddUserViewModel addUserViewModel)
    {
        if (addUserViewModel == null)
        {
            return null;
        }

        return new User
        {
            Username = addUserViewModel.Username,
            Password = PasswordEncrypter.Hash(addUserViewModel.Password),
            FirstName = addUserViewModel.FirstName,
            LastName = addUserViewModel.LastName,
            Email = addUserViewModel.Email,
            BirthDate = addUserViewModel.BirthDate.ToDateTime(TimeOnly.Parse("10:00 PM")),
            Gender = addUserViewModel.Gender,
            RoleId = addUserViewModel.RoleId,
            IsDeleted = false
        };
    }
}
