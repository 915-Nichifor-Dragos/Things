using HotelManagement.Models.ViewModels;

namespace HotelManagement.Models.DataModels;

public static class AccountDetailsMapper
{
    public static void MapAccountDetailsToUser(User user, MyAccountDetailsViewModel newUserDetails)
    {
        user.Email = newUserDetails.Email;
        user.Bio = newUserDetails.Bio;
        user.BirthDate = newUserDetails.BirthDate.ToDateTime(new TimeOnly(10, 00));
        user.Gender = newUserDetails.Gender;
        user.Address = newUserDetails.Address;
    }
}
