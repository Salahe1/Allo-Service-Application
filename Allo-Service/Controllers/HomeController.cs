using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Allo_Service.Models;
using Allo_Service.ViewModel;
using System.Security.Claims;
using System.Linq;

namespace PFA_Allo_Service.Controllers
{
    public class HomeController : Controller
    {
        MyContext db;
        public HomeController(MyContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Accueil()
        {
            // Fetch offres with the related Fournisseur data
            var offres = await db.Offres
                .Include(o => o.Fournisseur) // If you need Fournisseur data
                .ToListAsync();

            // Pass the list of offres to the view
            return View(offres);
        }


        public IActionResult ContactUs()
        {
            return View();
        }



    }
}

