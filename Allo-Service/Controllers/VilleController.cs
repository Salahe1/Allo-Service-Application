using Allo_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allo_Service.Controllers
{
    public class VilleController : Controller
    {

        //private static string Email;
        MyContext db;
        public VilleController(MyContext db)
        {
            this.db = db;
        }

        public IActionResult ListeVilles()
        {
            var villes = db.Villes.ToList(); // Fetch the list of villes from the database
            return View(villes);
        }

        [HttpPost]
        public async Task<IActionResult> AjouterVille([FromBody] Villes ville)
        {
            if (ModelState.IsValid)
            {
                db.Villes.Add(ville);
                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false, error = "Invalid data" });
        }
        public IActionResult supprimerVille()
        {
            return View();
        }
    }
}
