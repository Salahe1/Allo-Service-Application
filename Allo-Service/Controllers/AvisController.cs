using Allo_Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Allo_Service.Controllers
{
    public class AvisController : Controller
    {

        MyContext db;
        public AvisController(MyContext db)
        {
            this.db = db;
        }


        [HttpPost]
        public IActionResult SubmitRating(int fournisseurId, int rating)
        {
            var userId = HttpContext.Session.GetInt32("Id");

            if (userId == null)
            {
                return Json(new { success = false, message = "User not logged in" });
            }

            var avis = new Avis
            {
                FournisseurId = fournisseurId,
                ClientId = userId.Value, // Assuming that a Client is submitting the rating
                Note = rating,
                DateHeures = DateTime.Now
            };

            db.Avis.Add(avis);
            db.SaveChanges();

            // Optionally, return the new average rating and vote count to update the UI
            var averageRating = db.Avis.Where(a => a.FournisseurId == fournisseurId).Average(a => a.Note);
            var voteCount = db.Avis.Count(a => a.FournisseurId == fournisseurId);

            return Json(new { success = true, averageRating, voteCount });
        }

    }
}
