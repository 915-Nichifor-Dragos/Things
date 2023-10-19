using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;

namespace HotelManagement.BusinessLogic.Converters
{
    public static class BookingsConverter
    {
        public static Booking FromBookingViewModelToBooking(this BookingViewModel viewModel, Guid userId)
        {
            return new Booking
            {
                UserId = userId,
                HotelId = viewModel.Hotel.Id,
                RoomId = viewModel.Room.Id,
                StartDate = viewModel.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = viewModel.EndDate.ToDateTime(TimeOnly.MinValue),
                IsDeleted = false
            };
        }
        public static Booking FromBookingViewModelToBooking(this BookingViewModel viewModel)
        {
            return new Booking
            {
                HotelId = viewModel.Hotel.Id,
                RoomId = viewModel.Room.Id,
                StartDate = viewModel.StartDate.ToDateTime(TimeOnly.MinValue),
                EndDate = viewModel.EndDate.ToDateTime(TimeOnly.MinValue),
                IsDeleted = false
            };
        }

        public static BookingViewModel FromBookingToBookingViewModel(this Booking booking)
        {
            return new BookingViewModel
            {
                Id = booking.Id,
                Hotel = booking.Hotel.ToHotelInformationBooking(),
                Room = booking.Room.ToRoomInformationBooking(),
                StartDate = DateOnly.FromDateTime(booking.StartDate),
                EndDate = DateOnly.FromDateTime(booking.EndDate)
            };
        }

        public static HotelNameAndId ToHotelInformationBooking (this Hotel hotel)
        {
            return new HotelNameAndId
            {
                Id = hotel.Id,
                Name = hotel.Name,
            };
        }

        public static RoomInformation ToRoomInformationBooking(this Room room)
        {
            return new RoomInformation
            {
                Id = room.Id,
                Name = room.Name,
                Size = room.NumberOfPeople,
                Price = room.Price,
            };
        }

        public static List<HotelNameAndId> ToHotelInformationBooking(this List<Hotel> hotels)
        {
            return hotels.Select(h => h.ToHotelInformationBooking()).ToList();
        }

        public static List<RoomInformation> ToRoomInformationBooking(this List<Room> rooms)
        {
            return rooms.Select(r => r.ToRoomInformationBooking()).ToList();
        }
    }
}
