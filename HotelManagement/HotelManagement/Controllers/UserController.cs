using HotelManagement.BusinessLogic.Converters;
using HotelManagement.BusinessLogic.ILogic;
using HotelManagement.Models.Constants;
using HotelManagement.Models.DataModels;
using HotelManagement.Models.ViewModels;
using HotelManagement.Web.Authorize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelManagement.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class AccountController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IRoleLogic _roleLogic;
    private readonly IHotelLogic _hotelLogic;

    public AccountController(IUserLogic userLogic, IRoleLogic roleLogic, IHotelLogic hotelLogic)
    {
        _userLogic = userLogic;
        _roleLogic = roleLogic;
        _hotelLogic = hotelLogic;
    }

    [HttpGet]
    [AuthorizeRoles(Models.Constants.Role.Manager, Models.Constants.Role.Owner)]
    public async Task<IActionResult> GetUsers(string email)
    {
        var managerRole = await _roleLogic.GetRoleByName(Models.Constants.Role.Manager);
        var employeeRole = await _roleLogic.GetRoleByName(Models.Constants.Role.Employee);

        User user = await _userLogic.GetUserByEmail(email);

        var viewModel = UserToEditUserViewModelConverter.ConvertUser(user, new List<Models.DataModels.Role> { managerRole, employeeRole });

        return Ok(viewModel);
    }

    [HttpGet("by-all-hotels")]
    [AuthorizeRoles(Models.Constants.Role.Manager, Models.Constants.Role.Owner)]
    public async Task<ActionResult> GetUsersByAllHotels(UserListingSortType sortAttribute, bool isAscending, int? pageSize, int? pageIndex)
    {
        var authenticatedUsername = User.FindFirst(ClaimTypes.Name).Value;
        var authenticatedUser = await _userLogic.GetUserByUsernameWithHotels(authenticatedUsername);

        if (authenticatedUser == null)
        {
            return BadRequest(new HashSet<HotelSearchViewModel>());
        }

        var paginatedList = await _userLogic.GetAll(sortAttribute, isAscending, authenticatedUser, pageSize ?? 5, pageIndex ?? 1);

        var viewModel = new UserTableViewModel
        {
            Users = paginatedList,
        };

        return Ok(viewModel);
    }

    [HttpGet("by-hotel")]
    [AuthorizeRoles(Models.Constants.Role.Manager, Models.Constants.Role.Owner)]
    public async Task<ActionResult> GetUsersByHotel(Guid hotelId, UserListingSortType sortAttribute, bool isAscending, int? pageSize, int? pageIndex)
    {
        var hotel = await _hotelLogic.GetById(hotelId);

        if (hotel != null)
        {
            var paginatedUserList = await _userLogic.GetByHotelId(hotelId, sortAttribute, isAscending, User.IsInRole(Models.Constants.Role.Owner.ToString()), pageSize ?? 5, pageIndex ?? 1);

            var viewModel = new UserTableViewModel
            {
                Users = paginatedUserList,
            };

            return Ok(viewModel);
        }

        return BadRequest("Something went wrong");
    }

    [HttpPost]
    [AuthorizeRoles(Models.Constants.Role.Owner, Models.Constants.Role.Manager)]
    public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
    {
        var validationResults = addUserViewModel.Validate();

        var user = AddUserViewModelToUserConverter.ConvertedUser(addUserViewModel);

        if (!validationResults.Any())
        {
            var validityOutcome = _userLogic.CheckValidity(user);

            if (validityOutcome == UserValidityOutcomes.InvalidEmail)
            {
                return RedirectToAction("AddUser");
            }

            if (validityOutcome == UserValidityOutcomes.InvalidUsername)
            {
                return RedirectToAction("AddUser");
            }

            var host = HttpContext.Request.Host.Host;
            var port = HttpContext.Request.Host.Port ?? 80;

            await _userLogic.CreateUser(user, host, port);

            var hotel = await _hotelLogic.GetById(addUserViewModel.HotelId);

            await _hotelLogic.AddUserToHotel(hotel, user);
        }

        return RedirectToAction("AddUser");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(MyAccountDetailsViewModel userData)
    {
        if (ModelState.IsValid)
        {
            var host = HttpContext.Request.Host.Host;
            var port = HttpContext.Request.Host.Port ?? 80;

            if (await _userLogic.UpdateUserInfo(User.Identity.Name, userData, host, port))
            {
                return Ok("User info succesfully updated");
            }

            return BadRequest("Something went wrong");
        }

        return BadRequest("Model was not validated");
    }


    [HttpPut("role")]
    [AuthorizeRoles(Models.Constants.Role.Manager, Models.Constants.Role.Owner)]
    public async Task<IActionResult> EditUserRole(string username, Models.Constants.Role changedRole)
    {
        var user = await _userLogic.GetUserByUsername(username);

        if (user == null)
        {
            return BadRequest("Something went wrong");
        }

        Models.DataModels.Role newRole = await _roleLogic.GetRoleByName(changedRole);

        _userLogic.UpdateUserRole(user, newRole.Id);

        return Redirect("/UserManagement/Users");
    }

    [HttpPut("profile-picture")]
    public async Task<IActionResult> UpdateProfilePicture(IFormFile ProfilePicture)
    {
        if (ModelState.IsValid)
        {
            await _userLogic.UpdateProfilePicture(User.Identity.Name, ProfilePicture);

            return Ok("User profile picture succesfully updated");
        }

        return BadRequest("Model was not validated");
    }

    [HttpDelete("profile-picture")]
    public async Task<IActionResult> DeleteProfilePicture()
    {
        try
        {
            await _userLogic.DeleteProfilePicture(User.Identity.Name);
        }
        catch (Exception ex)
        {
            return BadRequest("Something went wrong");
        }

        return Ok("User profile picture succesfully deleted");
    }

    [HttpPost("check-email")]
    public async Task<IActionResult> CheckEmail(string email)
    {
        var user = await _userLogic.GetUserByUsername(User.Identity.Name);

        if (email.Equals(user.Email))
        {
            return Ok("Email was not changed");
        }

        if ((await _userLogic.CheckEmailUniqueness(email)).Equals(UserValidityOutcomes.Valid))
        {
            return Ok("Email was changed and it's correct");
        }

        return BadRequest("The email is invalid");
    }

    [HttpPost("check-age")]
    public IActionResult CheckAge(string birthDate)
    {
        var feedback = _userLogic.CheckAge(birthDate);

        return feedback switch
        {
            "True" => Ok("Over 18"),
            "False" => BadRequest("Not over 18"),
            _ => BadRequest(feedback),
        };
    }

    [HttpDelete]
    [AuthorizeRoles(Models.Constants.Role.Manager, Models.Constants.Role.Owner)]
    public async Task<IActionResult> DeleteUser(string email)
    {
        var authenticatedUsername = User.FindFirst(ClaimTypes.Name).Value;
        var user = await _userLogic.GetUserByEmail(email);

        var success = await _userLogic.DeleteUser(user.Id, authenticatedUsername);

        return Ok(new { success });
    }
}