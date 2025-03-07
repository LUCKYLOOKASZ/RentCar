namespace RentCar.Models;

public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty; // Marka (np. BMW)
    public string Model { get; set; } = string.Empty; // Model (np. X5)
    public int Year { get; set; } // Rok produkcji
    public double PricePerDay { get; set; } // Cena za dzień wypożyczenia
    public bool IsAvailable { get; set; } = true; // Czy auto jest dostępne?

    // Dodatkowe parametry techniczne
    public int HorsePower { get; set; } // Moc w KM
    public int Torque { get; set; } // Moment obrotowy (Nm)
    public int MaxSpeed { get; set; } // Maksymalna prędkość (km/h)
    public double Acceleration { get; set; } // Przyspieszenie 0-100 km/h (sekundy)

    public string Description { get; set; } = string.Empty; // Opis samochodu
}