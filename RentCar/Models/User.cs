namespace RentCar.Models;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty; // Imię i nazwisko
    public string Email { get; set; } = string.Empty; // Email kontaktowy
    public string PhoneNumber { get; set; } = string.Empty; // Telefon kontaktowy
    public string? Address { get; set; } // Adres zamieszkania (opcjonalnie)

    // Relacja z rezerwacjami
    public List<Reservation> Reservations { get; set; } = new();
}
