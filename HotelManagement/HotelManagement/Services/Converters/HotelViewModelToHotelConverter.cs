using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class HotelViewModelToHotelConverter
{
    public static Hotel ConvertHotelModel(this HotelViewModel hotelViewModel, User owner)
    {
        return new Hotel()
        {
            Name = hotelViewModel.Name,
            Location = hotelViewModel.Location,
            IsAvailable = hotelViewModel.IsAvailable,
            Description = hotelViewModel.Description,
            Owner = owner,
            OwnerId = owner.Id,
            IsDeleted = false
        };
    }
}
