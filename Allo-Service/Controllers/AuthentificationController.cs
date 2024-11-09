using Allo_Service.Models;
using Allo_Service.ViewModel;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Allo_Service.Controllers
{
    public class AuthentificationController : Controller
    {
        //private static string Email;
        MyContext db;
        public AuthentificationController(MyContext db)
        {
            this.db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserVm userVM)
        {
            if (ModelState.IsValid)
            {
                // Query to find the user if they are a Fournisseur or Client
                Users user = db.Users.Where(u => (u is Fournisseur || u is Client) && u.Email == userVM.Email).FirstOrDefault();

                if (user != null)
                {
                    byte[] salt = Convert.FromBase64String(user.Salt);

                    // Verify the password
                    bool passwordMatch = VerifyPassword(userVM.MotdePasse, user.MotDePasse, salt);

                    if (passwordMatch)
                    {
                        // Password is correct, proceed with login
                        // Set common session variables for the user
                        HttpContext.Session.SetInt32("Id", user.Id);
                        HttpContext.Session.SetString("Email", user.Email);
                        HttpContext.Session.SetString("Nom", user.Nom);
                        HttpContext.Session.SetString("Prenom", user.Prenom);

                        if (user is Fournisseur fournisseur)
                        {
                            HttpContext.Session.SetString("UserType", "Fournisseur");
                            // Set Fournisseur-specific session variables
                            HttpContext.Session.SetString("Photo", string.IsNullOrEmpty(fournisseur.Photo) ? "img.jpg" : fournisseur.Photo);
                            // Redirect to Fournisseur controller's Index action
                            return RedirectToAction("FournisseurHome", "Fournisseur");
                        }
                        else if (user is Client client)
                        {
                            HttpContext.Session.SetString("UserType", "Client");
                            // Redirect to Client controller's Index action
                            return RedirectToAction("Accueil", "Home");
                        }

                    }
                    else
                    {
                        // Handle invalid password case
                        // Authentication failed, display an error message
                        ViewBag.ErrorMessage = "Invalid email or password.";
                        return View();
                    }

                }
                else
                {
                    // Authentication failed, display an error message
                    ModelState.AddModelError("Email", "Invalid email or password .");

                    return View();
                }
            }

            // Model state is not valid, redisplay the login form with validation errors
            return View(userVM);
        }



        // Method to verify the password against the stored hash
        private bool VerifyPassword(string password, string hashedPassword, byte[] salt)
        {
            // Hash the input password with the same salt and parameters used during registration
            using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                // Set the same salt, iterations, memory size, and parallelism parameters
                // Ensure you retrieve the salt used when the password was hashed (you'll need to store it)
                hasher.Salt = salt;
                hasher.DegreeOfParallelism = 4; // Number of threads to use
                hasher.MemorySize = 1024; // In KB
                hasher.Iterations = 3; // Number of iterations

                // Compute the hash
                byte[] hash = hasher.GetBytes(32);

                // Compare the computed hash with the stored hashed password
                return Convert.ToBase64String(hash) == hashedPassword;
            }
        }



        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string e, string p)
        {
            return RedirectToAction("AdminHome","Admin");
        }

        public IActionResult Logout()
        {
             HttpContext.Session.Clear();
           // HttpContext.Session.Remove("MySession");

            var lastConnexion = DateTime.Now;

            HttpContext.Response.Cookies.Append("LastConnexion", lastConnexion.ToString(), new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            });

            return RedirectToAction("Accueil", "Home");
        }
    }
}
