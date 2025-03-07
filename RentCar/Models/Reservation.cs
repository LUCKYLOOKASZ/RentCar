namespace RentCar.Models;

public class Reservation
{
    public int Id { get; set; }

    // Powiązanie z samochodem
    public int CarId { get; set; }
    public Car Car { get; set; } = null!;

    // Powiązanie z użytkownikiem
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    // Szczegóły rezerwacji
    public DateTime StartDate { get; set; } // Data rozpoczęcia wynajmu
    public DateTime EndDate { get; set; } // Data zakończenia wynajmu
    public bool IsConfirmed { get; set; } = false; // Czy rezerwacja została potwierdzona?
}
