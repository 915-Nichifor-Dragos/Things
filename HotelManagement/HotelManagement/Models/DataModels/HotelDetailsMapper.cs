using HotelManagement.Models.ViewModels;

namespace HotelManagement.Models.DataModels
{
    public static class HotelDetailsMapper
    {
        public static void MapNewDetails(this Hotel hotel, HotelViewModel viewModel, string imageUrl)
        {
            hotel.Description = viewModel.Description;
            hotel.Location = viewModel.Location;
            hotel.Name = viewModel.Name;
            hotel.HasRestaurant = viewModel.HasRestaurant;
            hotel.HasFreeWiFi = viewModel.HasFreeWiFi;
            hotel.HasPool = viewModel.HasPool;
            hotel.HasParking = viewModel.HasParking;
            hotel.HasSauna = viewModel.HasSauna;
            hotel.IsAvailable = viewModel.IsAvailable;
            hotel.ImageUrl = imageUrl;
        }
    }
}
