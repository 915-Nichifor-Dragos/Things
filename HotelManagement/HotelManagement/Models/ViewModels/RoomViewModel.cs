using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.ViewModels;

public class RoomViewModel
{
    [Required]
    [Range(1, 6, ErrorMessage = "Number of people must be between 1 and 6.")]
    public int NumberOfPeople { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public int Price { get; set; }

    [Required]
    public bool AllowsSmoking { get; set; }

    [Required]
    public bool AllowsDogs { get; set; }

    [Required]
    public Guid HotelId { get; set; }

    [Required]
    public bool IsReserved { get; set; }

    public IEnumerable<ValidationResult> Validate()
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(this);

        Validator.TryValidateObject(this, context, validationResults, validateAllProperties: true);

        return validationResults;
    }
}
