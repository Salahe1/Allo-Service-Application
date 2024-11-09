using Allo_Service.Models;
using Allo_Service.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Allo_Service.Controllers
{
    public class OffreController : Controller
    {
        private readonly MyContext db;

        public OffreController(MyContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Offres()
        {
            var offres = await db.Offres
                .Include(o => o.Fournisseur) // Include Fournisseur data
                .ThenInclude(f => f.Ville)   // Include Ville data through Fournisseur
                .AsNoTracking()
                .Select(o => new
                {
                    o.Id,
                    o.Title,
                    Photo = "/OffresImages/" + o.Photo, // Prepend the image path here
                    o.Description,
                    o.Price,
                    DateFin = o.DateFin.ToString("dd MMM yyyy"), // Format the date here
                    FournisseurId = o.FournisseurId,
                    VilleName = o.Fournisseur.Ville.Name // Include Ville name directly
                })
                .ToListAsync();

            return View(offres);
        }

        public IActionResult Ajouter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ajouter(OffreVM model)
        {
            int? id = HttpContext.Session.GetInt32("Id");

            if (id == null)
            {
                return Unauthorized(); // Redirect to login or authorization error page
            }

            // Retrieve the user from the database, ensuring it's of type Fournisseur
            var user = db.Users
                .Where(u => u.Id == id.Value && u is Fournisseur)
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            // Since we've already filtered to ensure the user is a Fournisseur, we can safely cast
            Fournisseur fournisseur = (Fournisseur)user;

            // Validate the model at the start
            if (!ModelState.IsValid)
            {
                // If the model is not valid, return the view with the existing model to show validation errors
                return View(model);
            }

            // Map ViewModel to Model (Offre)
            var offre = new Offre
            {
                Title = model.Titre,
                Description = model.Description,
                Price = (int)model.Prix,
                DateDebut = model.DateDebut,
                DateFin = model.DateFin,
                FournisseurId = fournisseur.Id // Set the FournisseurId to the current user's ID
            };

            // Handle Photo Upload
            if (model.Photo != null &&
                (model.Photo.FileName.EndsWith(".jpeg") || model.Photo.FileName.EndsWith(".jpg") || model.Photo.FileName.EndsWith(".png"))
                && model.Photo.Length < 800000)
            {

                // Set the file name to be saved in the Offre model's Photo property
                offre.Photo = model.Photo.FileName;

                // Combine path to save the file in the desired directory
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "OffresImages", model.Photo.FileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(path));

                // Save the file asynchronously
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }

                // Add the offer to the database and save changes
                db.Offres.Add(offre);
                await db.SaveChangesAsync();

                // Redirect to the relevant page after successfully adding the offer
                return RedirectToAction("ListeOffresFournisseurCopy", "Offre");
            }
            else
            {
                // Add an error message to ModelState if the file does not meet the criteria
                ModelState.AddModelError(string.Empty, "Le fichier doit être une image JPEG, JPG ou PNG et ne doit pas dépasser 100000 octets.");
            }

            // Return the view with the current model to show validation errors
            return View(model);
        }

        public IActionResult Modifier(int id)
        {
            if (id != null)
            {
                var offre = db.Offres
                                 .Where(o => o.Id == id)
                                 .Include(o => o.Fournisseur)
                                 .ThenInclude(f => f.Ville)
                                 .FirstOrDefault();

                return View(offre);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Modifier(OffreVM vm)
        {

            return View();
        }

        public IActionResult Supprimer(int id)
        {
            return View();
        }

        public async Task<IActionResult> ListeOffresFournisseur(int id)
        {
            var offres = await db.Offres
                .Where(o => o.FournisseurId == id)
                .Include(o => o.Fournisseur) // Include Fournisseur data
                .ThenInclude(f => f.Ville)
                .AsNoTracking()
                .ToListAsync();

            return View(offres);
        }


        public IActionResult ListOffresMetier(int id)
        {
            var listoffres = db.Users
                               .OfType<Fournisseur>()
                               .Where(f => f.MetierId == id)
                               .SelectMany(f => f.Offres)
                               .Include(o => o.Fournisseur)
                               .ThenInclude(f => f.Ville)
                               .ToList();
            return View(listoffres);
        }


        public IActionResult AutreOffres(int id)
        {
            var listeOffres = db.Offres
                                .Where(o => o.FournisseurId == id)
                                .ToList();
            return View(listeOffres);
        }

        public async Task<IActionResult> ListeOffresFournisseurCopyAsync()
        {
            var id = HttpContext.Session.GetInt32("Id");
            var offres = await db.Offres
                                 .Where(o => o.FournisseurId == id)
                                 .Include(o => o.Fournisseur) // Include Fournisseur data
                                 .ThenInclude(f => f.Ville)
                                 .ToListAsync();

            return View(offres);
        }

    }
}
