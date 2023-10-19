using HotelManagement.Models.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.ViewModels;

public class HotelViewModel
{
    public Guid HotelId { get; set; }

    [Required]
    [RegularExpression(@"[a-zA-Z0-9]{2,100}$", ErrorMessage = "Names must be alphanumeric and must be between 2 and 100 characters")]
    public string Name { get; set; }

    [Required]
    [RegularExpression(@"[a-zA-Z0-9]{2,100}$", ErrorMessage = "Locations must be alphanumeric and must be between 2 and 100 characters")]
    public string Location { get; set; }

    [Required]
    public bool IsAvailable { get; set; }

    public int NumberOfEmployees { get; set; }

    [StringLength(1000, ErrorMessage = "Description can have at most 1000 characters")]
    public string? Description { get; set; }

    public double? Earnings { get; set; }

    public bool HasFreeWiFi { get; set; }

    public bool HasParking { get; set; }

    public bool HasPool { get; set; }

    public bool HasSauna { get; set; }

    public bool HasRestaurant { get; set; }

    public bool IsEditable { get; set; }

    public string? ImageUrl { get; set; }

    [BindProperty]
    [ProfilePictureValidation(ErrorMessage = "File format not supported!")]
    [FileSize(ErrorMessage = "File size is too big!")]
    public IFormFile ProfilePicture { get; set; }

    public IEnumerable<ValidationResult> Validate()
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(this);

        Validator.TryValidateObject(this, context, validationResults, validateAllProperties: true);

        return validationResults;
    }
}
