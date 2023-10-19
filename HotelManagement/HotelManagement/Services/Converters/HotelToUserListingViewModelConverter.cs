using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class HotelToUserListingViewModelConverter
{
    public static UserListingViewModel ConvertHotels(List<Hotel> hotels)
    {
        if (hotels == null)
        {
            return null;
        }

        return new UserListingViewModel
        { 
            Hotels = hotels 
        };
    }
}
