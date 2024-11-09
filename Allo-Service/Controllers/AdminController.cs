using Allo_Service.Models;
using Allo_Service.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Allo_Service.Controllers
{
    public class AdminController : Controller
    {
        //private static string Email;
        MyContext db;
        public AdminController(MyContext db)
        {
            this.db = db;
        }

        public IActionResult AdminHome()
        {
            return View();
        }

        public IActionResult AjouterAdmin()
        {
            var cityList = db.Villes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            ViewBag.CityList = cityList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AjouterAdmin(AdminVm av)
        {
            if (ModelState.IsValid)
            {
                var admin = new Administrateur
                {
                    Nom = av.Nom,
                    Prenom = av.Prenom,
                    CIN = av.CIN,
                    Telephone = av.Telephone,
                    Email = av.Email,
                    MotDePasse = av.MotDePasse,
                    villeId = av.villeId,
                    Role = av.Role.ToString()
                };

                db.Users.Add(admin);

                // Save changes asynchronously
                await db.SaveChangesAsync();

                // Redirect to the list view to refresh data from the database
                return RedirectToAction("listeAdministrateurs");
            }

            // Return the view with the current model if the model state is not valid
            return View(av);
        }

        // GET: Display the edit admin form
        public IActionResult ModifierAdmin(int id)
        {
            var admin = db.Users.OfType<Administrateur>().FirstOrDefault(a => a.Id == id);

            if (admin == null)
            {
                return NotFound();
            }

            var adminVm = new AdminVm
            {
                Id = admin.Id,
                Nom = admin.Nom,
                Prenom = admin.Prenom,
                CIN = admin.CIN,
                Telephone = admin.Telephone,
                Email = admin.Email,
                MotDePasse = admin.MotDePasse,
                villeId = (int)admin.villeId,
                Role = Enum.Parse<AdminVm.role>(admin.Role)
            };

            return View("AjouterAdmin", adminVm); // Use the same view
        }

        // POST: Update existing admin
        [HttpPost]
        public async Task<IActionResult> ModifierAdmin(AdminVm av)
        {
            if (ModelState.IsValid)
            {
                var admin = await db.Users.OfType<Administrateur>().FirstOrDefaultAsync(a => a.Id == av.Id);

                if (admin == null)
                {
                    return NotFound();
                }

                admin.Nom = av.Nom;
                admin.Prenom = av.Prenom;
                admin.CIN = av.CIN;
                admin.Telephone = av.Telephone;
                admin.Email = av.Email;
                admin.MotDePasse = av.MotDePasse;
                admin.villeId = av.villeId;
                admin.Role = av.Role.ToString();

                await db.SaveChangesAsync();

                return RedirectToAction("listeAdministrateurs");
            }

            return View("AjouterAdmin", av);
        }

        public async Task<IActionResult> listeAdministrateurs()
        {
            var admins = await db.Users
                .OfType<Administrateur>()
                .Include(u => u.Ville)
                .ToListAsync(); // Use ToListAsync for async execution

            return View(admins);
        }

    }
}
