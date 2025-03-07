using Microsoft.AspNetCore.Mvc;
using RentCar.Models;

namespace RentCar.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private static List<Car> cars = new()
        {
            new Car { Id = 1, Brand = "BMW", Model = "M3", Year = 2022, PricePerDay = 350, HorsePower = 510, Torque = 650, MaxSpeed = 290, Acceleration = 3.9, Description = "Sportowe BMW M3, idealne do szybkich przejażdżek!" },
            new Car { Id = 2, Brand = "Audi", Model = "RS6", Year = 2021, PricePerDay = 400, HorsePower = 600, Torque = 800, MaxSpeed = 305, Acceleration = 3.6, Description = "Audi RS6 Avant, potężne kombi z osiągami superauta." }
        };

        // 1️⃣ GET - Pobiera wszystkie auta
        [HttpGet]
        public IActionResult GetAllCars()
        {
            try
            {
                Console.WriteLine($"Żądanie GET /api/cars - liczba aut: {cars.Count}");
                return Ok(cars);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd w GET /api/cars: {ex.Message}");
                return StatusCode(500, $"Wewnętrzny błąd serwera: {ex.Message}");
            }
        }

        // 2️⃣ GET - Pobiera auto po ID
        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            Console.WriteLine($"Żądanie GET /api/cars/{id} - liczba aut: {cars.Count}");

            if (cars.Count == 0)
                return NotFound("Brak samochodów w bazie. Może trzeba dodać nowe?");

            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound($"Nie znaleziono auta o ID {id}.");

            return Ok(car);
        }

        // 3️⃣ POST - Dodaje nowe auto
        [HttpPost]
        public IActionResult AddCar([FromBody] Car newCar)
        {
            newCar.Id = cars.Count + 1;
            cars.Add(newCar);
            return CreatedAtAction(nameof(GetCarById), new { id = newCar.Id }, newCar);
        }

        // 4️⃣ PUT - Aktualizuje dane auta
        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] Car updatedCar)
        {
            Console.WriteLine($"Żądanie PUT /api/cars/{id} - liczba aut: {cars.Count}");

            if (cars.Count == 0)
                return NotFound("Brak samochodów w bazie. Może trzeba dodać nowe?");

            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound($"Nie znaleziono auta o ID {id}.");

            // Aktualizacja danych
            car.Brand = updatedCar.Brand;
            car.Model = updatedCar.Model;
            car.Year = updatedCar.Year;
            car.PricePerDay = updatedCar.PricePerDay;
            car.HorsePower = updatedCar.HorsePower;
            car.Torque = updatedCar.Torque;
            car.MaxSpeed = updatedCar.MaxSpeed;
            car.Acceleration = updatedCar.Acceleration;
            car.Description = updatedCar.Description;

            Console.WriteLine($"Zaktualizowano auto: {car.Brand} {car.Model}");
            return Ok(car);
        }

        // 5️⃣ DELETE - Usuwa auto po ID
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            Console.WriteLine($"Żądanie DELETE /api/cars/{id} - liczba aut: {cars.Count}");

            if (cars.Count == 0)
                return NotFound("Brak samochodów w bazie do usunięcia.");

            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
                return NotFound($"Nie znaleziono auta o ID {id}.");

            cars.Remove(car);
            Console.WriteLine($"Usunięto auto o ID {id}");
            return NoContent();
        }
        [HttpDelete("delete-all")]
        public IActionResult DeleteAllCars()
        {
            Console.WriteLine($"Żądanie DELETE /api/cars/delete-all - liczba aut przed usunięciem: {cars.Count}");

            if (cars.Count == 0)
                return NotFound("Brak samochodów w bazie do usunięcia.");

            cars.Clear(); // Usunięcie wszystkich aut

            Console.WriteLine("Wszystkie auta zostały usunięte.");
            return NoContent(); // Zwraca 204 No Content
        }
        
        [HttpPost("batch")]
        public IActionResult AddMultipleCars([FromBody] List<Car> newCars)
        {
            if (newCars == null || newCars.Count == 0)
                return BadRequest("Lista aut jest pusta!");

            foreach (var car in newCars)
            {
                car.Id = cars.Count + 1; // Generowanie ID
                cars.Add(car);
            }

            return Ok(newCars); // Zwracamy dodane auta
        }
    }
}
