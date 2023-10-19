using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters;

public static class BookingToReservationListingViewModel
{
    public static ReservationListingViewModel ConvertReservation(Booking booking)
    {
        if (booking == null)
        {
            return null;
        }

        return new ReservationListingViewModel
        {
            Hotel = booking.Hotel.Name,
            Room = booking.Room.Name,
            StartDate = DateOnly.FromDateTime(booking.StartDate),
            EndDate = DateOnly.FromDateTime(booking.EndDate),
            ReservationId = booking.Id
        };
    }
}
