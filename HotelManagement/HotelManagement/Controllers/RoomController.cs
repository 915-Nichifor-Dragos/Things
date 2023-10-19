namespace HotelManagement.Controllers;

using HotelManagement.BusinessLogic.Converters;
using HotelManagement.BusinessLogic.ILogic;
using HotelManagement.Models.Constants;
using HotelManagement.Models.ViewModels;
using HotelManagement.Web.Authorize;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

[ApiController]
[Route("api/rooms")]
public class RoomController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IHotelLogic _hotelLogic;
    private readonly IRoomLogic _roomLogic;
    private readonly IBookingLogic _bookingLogic;

    public RoomController(IUserLogic userLogic, IHotelLogic hotelLogic, IRoomLogic roomLogic, IBookingLogic bookingLogic)
    {
        _userLogic = userLogic;
        _hotelLogic = hotelLogic;
        _roomLogic = roomLogic;
        _bookingLogic = bookingLogic;
    }


    [HttpGet]
    [AuthorizeRoles(Role.Manager, Role.Owner)]
    public async Task<ActionResult> GetReservations(ReservationListingSortType sortAttribute, bool isAscending, int? pageSize, int? pageIndex)
    {
        var authenticatedUsername = User.FindFirst(ClaimTypes.Name).Value;

        var paginatedUserList = await _bookingLogic.SearchReservations(sortAttribute, isAscending, pageSize ?? 5, pageIndex ?? 1, authenticatedUsername);

        var viewModel = new ReservationTableViewModel
        {
            Reservations = paginatedUserList,
        };

        return Ok(viewModel);
    }


    [HttpPost]
    [AuthorizeRoles(Role.Owner)]
    public async Task<IActionResult> AddRoom(RoomViewModel roomViewModel)
    {
        IEnumerable<ValidationResult> validationResults = roomViewModel.Validate();

        if (validationResults.Any())
        {
            return BadRequest("Model is incorrect");
        }

        var hotel = await _hotelLogic.GetById(roomViewModel.HotelId);

        if (hotel != null)
        {
            int roomNumber = await _hotelLogic.GetNextHotelRoomNumber(roomViewModel.HotelId);
            _roomLogic.AddRoom(RoomViewModelToRoomConverter.ConvertRoom(roomViewModel, roomNumber));

            return Ok("Succesfully added hotel");
        }

        return BadRequest("Room does not exist");
    }

    [HttpDelete]
    [AuthorizeRoles(Role.Manager, Role.Owner)]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        var authenticatedUsername = User.FindFirst(ClaimTypes.Name).Value;

        var success = await _bookingLogic.Delete(id, authenticatedUsername);

        return Ok(success);
    }

    [HttpGet("price-interval")]
    public async Task<IActionResult> GetRoomPriceForInterval(Guid roomId)
    {
        var room = await _roomLogic.GetById(roomId);

        return Ok(room.ToRoomInformationBooking());
    }
}
