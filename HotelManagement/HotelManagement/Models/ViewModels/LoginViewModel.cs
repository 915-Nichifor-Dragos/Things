using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.ViewModels;

public class LoginViewModel
{
    [Required]
    [RegularExpression(@"[a-zA-Z0-9]{4,30}$", ErrorMessage = "Usernames must be alphanumeric and must be between 4 and 30 characters")]
    public string Username { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 8, ErrorMessage = "Passwords must be between 8 and 30 characters")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$%^&*!])[^\s]+$", ErrorMessage = "Passwords can " +
        "not contain white spaces and must have at least 1 lowercase letter, 1 digit and 1 special character")]
    public string Password { get; set; }

    public IEnumerable<ValidationResult> Validate()
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(this);

        Validator.TryValidateObject(this, context, validationResults, validateAllProperties: true);

        return validationResults;
    }
}
