using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class RoomViewModelToRoomConverter
{
    public static Room ConvertRoom(RoomViewModel roomViewModel, int roomNumber)
    {
        return new Room()
        {
            Name = roomNumber,
            HotelId = roomViewModel.HotelId,
            NumberOfPeople = roomViewModel.NumberOfPeople,
            Price = roomViewModel.Price,
            AllowsDogs = roomViewModel.AllowsDogs,
            AllowsSmoking = roomViewModel.AllowsSmoking,
            IsDeleted = false
        };
    }
}
