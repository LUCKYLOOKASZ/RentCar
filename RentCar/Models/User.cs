using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models;

[Table("titiUsers")]  // Mapowanie na tabelę w bazie danych
public class User
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; }

    [Required(ErrorMessage = "Full Name is required")]
    [StringLength(50, ErrorMessage = "Full Name must be between 3 and 50 characters", MinimumLength = 3)]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [StringLength(20, ErrorMessage = "Username must be between 3 and 20 characters", MinimumLength = 3)]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, ErrorMessage = "Password must be at least 6 characters", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^\d{9,15}$", ErrorMessage = "Phone number must be between 9 and 15 digits")]
    public string PhoneNumber { get; set; }

    public string? Address { get; set; }
}