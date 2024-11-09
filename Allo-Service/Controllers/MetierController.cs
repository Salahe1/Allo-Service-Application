using Allo_Service.Models;
using Allo_Service.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allo_Service.Controllers
{
    public class MetierController : Controller
    {
        MyContext db;
        public MetierController(MyContext db)
        {
            this.db = db;
        }

        public IActionResult Métier()
        {
            var listeMétiers = db.Metiers.AsNoTracking().ToList();

            return View(listeMétiers);
        }


        public IActionResult liste()
        {
            var listesMetiers = db.Metiers.ToList();
            return View(listesMetiers);
        }

        public IActionResult ajouter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ajouter(String MetierNom, IFormFile formFile)
        {
            var Metier = new Metier
            {
                Name = MetierNom
            };
            if (formFile != null &&
                 (formFile.FileName.EndsWith(".jpeg") || formFile.FileName.EndsWith(".jpg") || formFile.FileName.EndsWith(".png"))
              && formFile.Length < 2800000)
            {

                // Generate a timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                // Set the file name to be saved in the Offre model's Photo property
                string uniqueFileName = $"{timestamp}_{formFile.FileName}";
              
                // Set the file name to be saved in the Offre model's Photo property
                Metier.Photo = uniqueFileName;

                // Combine path to save the file in the desired directory
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImagesMétiers", formFile.FileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                // Save the file asynchronously
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                // Add the offer to the database and save changes
                db.Metiers.Add(Metier);
                await db.SaveChangesAsync();

                // Redirect to the relevant page after successfully adding the offer
                return RedirectToAction("Offre", "Home");
            }
            else
            {
                // Add an error message to ModelState if the file does not meet the criteria
                ModelState.AddModelError(string.Empty, "Le fichier doit être une image JPEG, JPG ou PNG et ne doit pas dépasser 100000 octets.");
            }
            return View(liste);
        }


        public IActionResult Modifier(int id)
        {
            var metier = db.Metiers.FirstOrDefault(m => m.Id == id);

            return View(metier);
        }
        [HttpPost]
        public async Task<IActionResult> Modifier(int Id,String MetierNom, IFormFile formFile)
        {
            var metier = db.Metiers.FirstOrDefault(m => m.Id == Id);
            if (metier == null)
            {
                return null;
            }

            metier.Name = MetierNom;

            if (formFile != null &&
                (formFile.FileName.EndsWith(".jpeg") || formFile.FileName.EndsWith(".jpg") || formFile.FileName.EndsWith(".png"))
             && formFile.Length < 2800000)
            {
                // Set the file name to be saved in the Offre model's Photo property
                metier.Photo = formFile.FileName;

                // Combine path to save the file in the desired directory
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ImagesMétiers", formFile.FileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                // Save the file asynchronously
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                // Add the offer to the database and save changes
                db.Metiers.Update(metier);
                await db.SaveChangesAsync();

                // Redirect to the relevant page after successfully adding the offer
                return RedirectToAction("Offre", "Home");
            }


            return View();
        }


        public IActionResult supprimerMetiers()
        {
            return View();
        }



      
    }
}
