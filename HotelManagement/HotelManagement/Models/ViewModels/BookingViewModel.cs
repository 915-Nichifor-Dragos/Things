using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.ViewModels;

public class BookingViewModel
{
    public Guid Id { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    public HotelNameAndId Hotel { get; set; }

    public RoomInformation Room { get; set; }

    public IEnumerable<ValidationResult> Validate()
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(this);

        Validator.TryValidateObject(this, context, validationResults, validateAllProperties: true);

        if (EndDate < StartDate)
        {
            validationResults.Add(new ValidationResult("End Date should be higher than startdate!"));
        }

        if (EndDate.Day - StartDate.Day == 0)
        {
            validationResults.Add(new ValidationResult("EndDate should not be equal to StartDate"));
        }

        return validationResults;
    }
}
