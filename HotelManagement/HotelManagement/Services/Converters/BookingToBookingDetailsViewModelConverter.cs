using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class BookingToBookingDetailsViewModelConverter
{
    public static BookingManagementViewModel ConvertBooking(this Booking booking)
    {
        TimeSpan timeSpan = booking.EndDate - booking.StartDate;

        return new BookingManagementViewModel()
        {
            Id = booking.Id,
            Hotel = booking.Hotel.Name,
            Room = booking.Room.Name.ToString(),
            Days = (int)(timeSpan.TotalDays),
            PricePerNight = booking.Room.Price,
            TotalPrice = booking.Room.Price * (int)(timeSpan.TotalDays),
            EndDate = DateOnly.FromDateTime(booking.EndDate)
        };
    }
}
