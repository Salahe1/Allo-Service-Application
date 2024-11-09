using Allo_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allo_Service.Controllers
{
    public class FournisseurController : Controller
    {
        //private static string Email;
        MyContext db;
        public FournisseurController(MyContext db)
        {
            this.db = db;
        }


        public IActionResult FournisseurHome()
        {
            return View();
        }

        public IActionResult Fournisseur()
        {
            var listeFournisseur = db.Users
                                     .OfType<Fournisseur>() // Fetch only Fournisseur objects
                                     .Include(f => f.metier)
                                     .Include(u => u.Ville)
                                     .Include(a => a.Avis)
                                     .AsNoTracking()
                                     .ToList();
            // Iterate through the list and assign the Metier's Photo to Fournisseur's Photo if it's null
            foreach (var fournisseur in listeFournisseur)
            {
                if (string.IsNullOrEmpty(fournisseur.Photo) )
                {
                    fournisseur.Photo = "/ImagesMétiers/"+fournisseur.metier.Photo; // Use Metier's Photo or a default photo if null
                }  
                
                if (string.IsNullOrEmpty(fournisseur.Description) )
                {
                    fournisseur.Description = "No Description"; // Use Metier's Photo or a default photo if null
                }

            }



            return View(listeFournisseur);
        }




        public IActionResult ListFournisseursMetier(int id)
        {
            var listfournisseur = db.Users
                                    .OfType<Fournisseur>()
                                    .Where(f => f.MetierId == id)
                                    .Include(m => m.metier)
                                    .Include(v => v.Ville)
                                     .Include(a => a.Avis)
                                    .ToList();

            foreach (var fournisseur in listfournisseur)
            {
                if (string.IsNullOrEmpty(fournisseur.Photo))
                {
                    fournisseur.Photo = "/ImagesMétiers/" + fournisseur.metier.Photo; // Use Metier's Photo or a default photo if null
                }

                if (string.IsNullOrEmpty(fournisseur.Description))
                {
                    fournisseur.Description = "No Description"; // Use Metier's Photo or a default photo if null
                }

            }
            return View(listfournisseur);
        }



    }
}
