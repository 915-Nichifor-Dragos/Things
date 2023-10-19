using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class HotelsConverter
{
    public static HotelsInformationViewModel FromHotelToHotelsInformationViewModel(ICollection<Hotel> hotels)
    {
        return new HotelsInformationViewModel
        {
            Hotels = hotels.Select(FromHotelToHotelInfo).ToList()
        };
    }

    public static HotelInfo FromHotelToHotelInfo(Hotel hotel)
    {
        return new HotelInfo
        {
            Id = hotel.Id,
            Location = hotel.Location,
            IsAvailable = hotel.IsAvailable,
            Name = hotel.Name,
            NumberOfEmployees = hotel.NumberOfEmployees,
        };
    }

    public static HotelViewModel FromHotelToHotelViewModel(this Hotel hotel, bool editable)
    {
        return new HotelViewModel
        {
            HotelId = hotel.Id,
            Description = hotel.Description,
            HasFreeWiFi = hotel.HasFreeWiFi,
            HasParking = hotel.HasParking,
            HasPool = hotel.HasPool,
            HasRestaurant = hotel.HasRestaurant,
            HasSauna = hotel.HasSauna,
            IsAvailable = hotel.IsAvailable,
            Name = hotel.Name,
            ImageUrl = hotel.ImageUrl,
            Location = hotel.Location,
            Earnings = hotel.Bookings.Sum(b => b.Room.Price * (b.EndDate - b.StartDate).Days),
            NumberOfEmployees = hotel.UserHotels.Count,
            IsEditable = editable
        };
    }
    public static Hotel FromHotelViewModelToHotel(HotelViewModel hotel)
    {
        return new Hotel
        {
            Description = hotel.Description,
            HasFreeWiFi = hotel.HasFreeWiFi,
            HasParking = hotel.HasParking,
            HasPool = hotel.HasPool,
            HasRestaurant = hotel.HasRestaurant,
            HasSauna = hotel.HasSauna,
            IsAvailable = hotel.IsAvailable,
            Name = hotel.Name,
            ImageUrl = hotel.ImageUrl,
            Location = hotel.Location,
            Earnings = hotel.Earnings,
            NumberOfEmployees = hotel.NumberOfEmployees,
        };
    }
}