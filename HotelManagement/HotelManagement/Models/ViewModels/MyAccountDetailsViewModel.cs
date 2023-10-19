﻿using HotelManagement.Models.Constants;
using HotelManagement.Models.Validators;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.ViewModels;

public class MyAccountDetailsViewModel
{
    public Guid? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required]
    [DateAfter1900(ErrorMessage = "Date should be after 1900")]
    [IsOver18]
    public DateOnly BirthDate { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [StringLength(100, ErrorMessage = "Address can have at most 100 characters")]
    public string? Address { get; set; }

    [StringLength(1000, ErrorMessage = "Bio can have at most 1000 characters")]
    public string? Bio { get; set; }

    public string? ImageUrl { get; set; }

    [BindProperty]
    [ProfilePictureValidation(ErrorMessage = "File format not supported!")]
    [FileSize(ErrorMessage = "File size is too big!")]
    public IFormFile? ProfilePicture { get; set; }

    [SwaggerSchema("RoleId")]
    public DataModels.Role? Role { get; set; }

}
