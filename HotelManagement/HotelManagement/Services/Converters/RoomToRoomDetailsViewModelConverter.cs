using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class RoomToRoomDetailsViewModelConverter
{
    public static RoomDetailsViewModel ConvertRoom(this Room room)
    {
        return new RoomDetailsViewModel()
        {
            Number = room.Name,
            People = room.NumberOfPeople,
            Price = room.Price,
            AllowsSmoking = room.AllowsSmoking,
            AllowsDogs = room.AllowsDogs,
            IsReserved = room.Bookings.Any(b => b.StartDate <= DateTime.UtcNow && DateTime.UtcNow <= b.EndDate), 
        };
    }
}
