using Microsoft.AspNetCore.Mvc;
using RentCar.Data;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using RentCar.Models;

namespace RentCar.Controllers
{
    public class UserController : Controller
    {
        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }
    
        public IActionResult Test()
        {
            var Users = _context.Users.ToList();
            return Json(Users);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ManageUsers()
        {
            var Users = _context.Users.ToList();
            return View(Users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditUser(string username)
        {
            var Users = _context.Users.ToList();
            var user = Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditUser(string username, string newPassword)
        {
            var Users = _context.Users.ToList();
            var user = Users.FirstOrDefault(u => u.UserName == username);
            if (user != null)
            {
                user.Password = newPassword;
                ViewBag.Message = "Password updated successfully!";
            }
            else
            {
                ViewBag.Error = "User not found!";
            }
            return RedirectToAction("ManageUsers");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                TempData["Error"] = "Invalid username!";
                return RedirectToAction("ManageUsers");
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                TempData["Error"] = "User not found!";
            }
            else
            {
                _context.Users.Remove(user);  // Usuwanie z bazy
                _context.SaveChanges();       // Zapisanie zmian
                TempData["Message"] = "User deleted successfully!";
            }

            return RedirectToAction("ManageUsers");
        }


        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var Users = _context.Users.ToList();
            var user = Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, username == "admin" ? "Admin" : "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
            
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            // Sprawdzamy, czy użytkownik o tej nazwie już istnieje w bazie
            if (_context.Users.Any(u => u.UserName == model.UserName))
            {
                ViewBag.Error = "Username already exists.";
                return View(model); // Jeśli tak, wracamy do widoku z komunikatem
            }

            
            // Haszowanie hasła Puki co wywalone
            // var passwordHasher = new PasswordHasher<User>();
            // var hashedPassword = passwordHasher.HashPassword(null, model.Password);

            // Tworzymy nowego użytkownika
            var newUser = new User
            {
                Role = "User",
                UserName = model.UserName,
                Password = model.Password,  
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            // Dodajemy użytkownika do kontekstu
            _context.Users.Add(newUser);

            // Zapisujemy zmiany w bazie danych
            _context.SaveChanges();

            // Informujemy o sukcesie
            ViewBag.Success = "User registered successfully!";
            return RedirectToAction("Login");
        }
        
        
        
        
    }    
}

