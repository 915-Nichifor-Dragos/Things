using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models.DataModels;

public class Hotel
{

    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Location { get; set; }

    [Required]
    public bool IsAvailable { get; set; }

    public int NumberOfEmployees { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public Guid? OwnerId { get; set; }

    public User? Owner { get; set; }

    public double? Earnings { get; set; }

    public bool HasFreeWiFi { get; set; }

    public bool HasParking { get; set; }

    public bool HasPool { get; set; }

    public bool HasSauna { get; set; }

    public bool HasRestaurant { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<UserHotels> UserHotels { get; set; } = new HashSet<UserHotels>();

    public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();

    public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
}
