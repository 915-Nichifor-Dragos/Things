using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class UserViewModelToUserConverter
{
    public static User ConvertedUser(UserViewModel userViewModel, int roleId)
    {
        if (userViewModel == null)
        {
            return null;
        }

        return new User
        {
            Username = userViewModel.Username,
            Password = PasswordEncrypter.Hash(userViewModel.Password),
            FirstName = userViewModel.FirstName,
            LastName = userViewModel.LastName,
            Email = userViewModel.Email,
            BirthDate = userViewModel.BirthDate.ToDateTime(TimeOnly.Parse("10:00 PM")),
            Gender = userViewModel.Gender,
            RoleId = roleId,
            Address = userViewModel.Address,
            Bio = userViewModel.Bio,
            ImageUrl = userViewModel.ImageUrl,
            IsActive = userViewModel.IsActive,
            ActivationToken = userViewModel.ActivationToken,
            TokenGenerationTime = userViewModel.TokenGenerationTime,
        };
    }
}
