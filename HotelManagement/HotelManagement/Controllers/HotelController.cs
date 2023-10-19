namespace HotelManagement.Controllers;

using HotelManagement.BusinessLogic.Converters;
using HotelManagement.BusinessLogic.ILogic;
using HotelManagement.Models.Constants;
using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;
using HotelManagement.Web.Authorize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

[ApiController]
[Route("api/hotels")]
public class HotelController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IHotelLogic _hotelLogic;
    private readonly IRoomLogic _roomLogic;

    public HotelController(IUserLogic userLogic, IHotelLogic hotelLogic, IRoomLogic roomLogic)
    {
        _userLogic = userLogic;
        _hotelLogic = hotelLogic;
        _roomLogic = roomLogic;
    }

    [HttpGet]
    [AuthorizeRoles(Models.Constants.Role.Owner)]
    public async Task<IActionResult> GetHotels(int? pageNumber, int? pageSize, string sortParam, bool isDescending)
    {
        var user = await _userLogic.GetUserByUsername(User.Identity.Name);

        if (user != null)
        {
            var paginatedListOfHotels = await _hotelLogic.GetHotelsByOwner(user, pageNumber ?? 1, pageSize ?? 5, sortParam ?? "Name", isDescending);

            return Ok(paginatedListOfHotels);
        }

        return BadRequest("You are not logged in");
    }

    [HttpGet("limit")]
    [AuthorizeRoles(Models.Constants.Role.Manager, Models.Constants.Role.Owner)]
    public async Task<IActionResult> GetHotelsLimited()
    {
        var authenticatedUsername = User.FindFirst(ClaimTypes.Name).Value;
        var authenticatedUser = await _userLogic.GetUserByUsernameWithHotels(authenticatedUsername);

        if (authenticatedUser == null)
        {
            return BadRequest(new HashSet<HotelSearchViewModel>());
        }

        var hotels = await _hotelLogic.GetAll(20, authenticatedUser);

        var viewModel = HotelToUserListingViewModelConverter.ConvertHotels(hotels);

        return Ok(viewModel);
    }


    [HttpGet("available-between-dates")]
    [Authorize]
    public async Task<List<HotelNameAndId>> GetAvailableHotels(AvailableHotelsRequestModel dates)
    {
        var hotels = await _hotelLogic.GetAvailableHotels(dates.StartDate, dates.EndDate);

        return hotels.ToHotelInformationBooking();
    }

    [HttpGet("rooms")]
    public async Task<IActionResult> GetRoomsByHotel(Guid hotelId, RoomListingSortType sortAttribute, bool isAscending, int? pageSize, int? pageIndex, bool allowsSmoking, bool allowsDogs)
    {
        var hotel = await _hotelLogic.GetById(hotelId);

        if (hotel != null)
        {
            var paginatedRoomList = await _roomLogic.GetByHotelId(hotelId, sortAttribute, isAscending, pageSize ?? 5, pageIndex ?? 1, allowsSmoking, allowsDogs);

            var viewModel = new RoomTableViewModel
            {
                Rooms = paginatedRoomList,
            };

            return Ok(viewModel);
        }

        return BadRequest("Hotel does not exist");
    }

    [HttpGet("available-rooms")]
    public async Task<List<RoomInformation>> GetAvailableRooms(AvailableRoomsViewModel model)
    {
        var rooms = await _roomLogic.GetAvailableRooms(model.HotelId, model.StartDate, model.EndDate);

        return rooms.ToRoomInformationBooking();
    }

    [HttpGet("booking-hotels")]
    [AuthorizeRoles(Models.Constants.Role.Manager, Models.Constants.Role.Owner)]
    public async Task<IActionResult> GetBookingHotels()
    {
        var authenticatedUsername = User.FindFirst(ClaimTypes.Name).Value;
        var authenticatedUser = await _userLogic.GetUserByUsernameWithHotels(authenticatedUsername);

        if (authenticatedUser == null)
        {
            return Ok(new HashSet<HotelSearchViewModel>());
        }

        var hotels = await _hotelLogic.GetAll(20, authenticatedUser);

        var viewModel = HotelToUserListingViewModelConverter.ConvertHotels(hotels);

        return Ok(viewModel);
    }

    [HttpPost]
    [AuthorizeRoles(Models.Constants.Role.Owner)]
    public async Task<IActionResult> AddHotel(HotelViewModel hotelViewModel)
    {
        var user = await _userLogic.GetUserByUsername(User.Identity.Name);

        if (user != null)
        {
            await _hotelLogic.AddHotel(hotelViewModel.ConvertHotelModel(user), hotelViewModel.ProfilePicture);

            return Ok("Succesfully added hotel");
        }

        return BadRequest("You are not logged in");
    }

    [HttpPut]
    [AuthorizeRoles(Models.Constants.Role.Owner)]
    public async Task<IActionResult> UpdateHotel(HotelViewModel hotelViewModel)
    {
        IEnumerable<ValidationResult> validationResults = hotelViewModel.Validate();

        if (!validationResults.Any())
        {
            var hotel = await _hotelLogic.GetById(hotelViewModel.HotelId);

            if (hotel == null)
            {
                return BadRequest("Hotel does not exist");
            }

            var imageUrl = await _hotelLogic.UploadProfilePicture(hotelViewModel.ProfilePicture);
            hotel.MapNewDetails(hotelViewModel, imageUrl);

            await _hotelLogic.UpdateHotel(hotel);

            return Ok("Succesfully updated hotel");
        }

        return BadRequest("Model was invalid");
    }

    [HttpDelete("profile-picture")]
    [AuthorizeRoles(Models.Constants.Role.Owner)]
    public async Task<IActionResult> DeleteHotelPicture(Guid id)
    {
        try
        {
            await _hotelLogic.DeletePicture(id);
        }
        catch (Exception ex)
        {
            return RedirectToAction("EditHotel", new { id = id, editable = true });
        }

        return RedirectToAction("EditHotel", new { id = id, editable = true });
    }
}
