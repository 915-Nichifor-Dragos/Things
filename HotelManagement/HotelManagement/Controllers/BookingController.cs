using HotelManagement.BusinessLogic.Converters;
using HotelManagement.BusinessLogic.ILogic;
using HotelManagement.Models.Constants;
using HotelManagement.Models.ViewModels;
using HotelManagement.Web.Authorize;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelManagement.Controllers;

[AuthorizeRoles(Role.Client)]
[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IBookingLogic _bookingLogic;

    public BookingsController(IUserLogic userLogic, IBookingLogic bookingLogic)
    {
        _userLogic = userLogic;
        _bookingLogic = bookingLogic;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings(BookingSortType sortOn, bool isAscending, int? pageSize, int? pageIndex)
    {
        var authenticatedUsername = User.FindFirst(ClaimTypes.Name).Value;
        var authenticatedUser = await _userLogic.GetUserByUsername(authenticatedUsername);

        if (authenticatedUser == null)
        {
            return BadRequest("You are not logged in");
        }

        var bookings = await _bookingLogic.GetBookingsByUser(authenticatedUser, sortOn, isAscending, pageSize ?? 5, pageIndex ?? 1);
        var viewModel = new BookingTableViewModel()
        {
            Bookings = bookings
        };

        return Ok(viewModel);
    }

    [HttpPost("book-room")]
    public async Task<IActionResult> BookRoom(Guid id)
    {
        if (id.Equals(Guid.Empty))
        {
            return BadRequest("No id sent");
        }
        var booking = await _bookingLogic.GetById(id);

        if (booking == null)
        {
            return BadRequest("Booking does not exist");
        }

        return Ok(booking.FromBookingToBookingViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdateBooking(BookingViewModel bookingViewModel)
    {
        var results = bookingViewModel.Validate();

        if (!results.Any())
        {
            if (bookingViewModel.Id == Guid.Empty)
            {
                var user = await _userLogic.GetUserByUsername(User.Identity.Name);

                if (user == null)
                {
                    return BadRequest("You are not logged in");
                }

                string host = HttpContext.Request.Host.Host;
                int port = HttpContext.Request.Host.Port ?? 80;

                try
                {
                    await _bookingLogic.AddBooking(bookingViewModel.FromBookingViewModelToBooking(user.Id), user, host, port);

                    return Ok("Succesfully added booking");
                }
                catch (Exception ex)
                {
                    return BadRequest("Something went wrong");
                }
            }
            else
            {
                try
                {
                    var booking = await _bookingLogic.GetById(bookingViewModel.Id);

                    if (booking == null)
                    {
                        return BadRequest("Booking does not exist!");
                    }

                    booking.RoomId = bookingViewModel.Room.Id;

                    await _bookingLogic.UpdateBooking(booking);

                    return Ok("Succesfully updated booking");
                }
                catch (Exception ex)
                {
                    return BadRequest("Something went wrong");
                }
            }
        }

        return BadRequest("Something went wrong!");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBooking(Guid id)
    {
        var authenticatedUsername = User.FindFirst(ClaimTypes.Name).Value;

        var success = await _bookingLogic.Delete(id, authenticatedUsername);

        return Ok(success);
    }
}