using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Allo_Service.Models;
using Allo_Service.Services;
using Allo_Service.ViewModel;
using Microsoft.EntityFrameworkCore;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Allo_Service.Controllers
{
    public class UsersController : Controller
    {
        //private static string Email;
        private readonly MyContext db;
        private readonly EmailService _emailService;
        public UsersController(MyContext db, EmailService _emailService)
        {
            this.db = db;
            this._emailService = _emailService; // Consider using Dependency Injection for better practice
        }

        public IActionResult ModifierProfil()
        {
            Users user = db.Users.Where(u => (u is Fournisseur || u is Client) && u.Id == HttpContext.Session.GetInt32("Id")).FirstOrDefault();
            ProfilModificationVM model = new ProfilModificationVM
            {
                Nom = user.Nom,
                Prenom = user.Prenom,
                Email = user.Email,
                Telephone = user.Telephone,
                CIN = user.CIN,
                villeId = (int)user.villeId,

                Villes = db.Villes.Select(v => new SelectListItem
                {
                    Value = v.Id.ToString(),
                    Text = v.Name
                }).ToList(),
                Metiers = db.Metiers.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                }).ToList()
            };

            if (user is Fournisseur fournisseur)
            {
                model.disponibitlité = (bool)fournisseur.Disponibiliter;
                model.metierId = (int)fournisseur.MetierId;
                model.description = fournisseur.Description;
            }


            return View(model);
        }
        [HttpPost]
        public IActionResult ModifierProfil(ProfilModificationVM vm)
        {
            if (HttpContext.Session.GetString("UserType") == "Client")
            {
                ModelState.Remove("metierId");
                ModelState.Remove("disponibitlité");
                ModelState.Remove("description");
            }

            if (ModelState.IsValid)
            {
                int id = (int)HttpContext.Session.GetInt32("Id");
                var user = db.Users.FirstOrDefault(u => u.Id == id);

                if (user != null)
                {

                    user.Nom = vm.Nom;
                    user.Prenom = vm.Prenom;
                    user.Email = vm.Email;
                    user.CIN = vm.CIN;
                    user.Telephone = vm.Telephone;
                    user.villeId = vm.villeId;  

                    HttpContext.Session.SetString("Nom", user.Nom);
                    HttpContext.Session.SetString("Prenom", user.Prenom);
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("CIN", user.CIN);
                    HttpContext.Session.SetString("Telephone", user.Telephone);
                    HttpContext.Session.SetInt32("VilleId", (int)user.villeId);

                    if (user is Fournisseur fournisseur)
                    {
                        fournisseur.Disponibiliter = vm.disponibitlité;
                        fournisseur.MetierId = vm.metierId;

                        if (vm.photo != null)
                        {
                            if ((vm.photo.FileName.EndsWith(".jpeg") || vm.photo.FileName.EndsWith(".jpg") || vm.photo.FileName.EndsWith(".png"))
                            && vm.photo.Length < 2800000)
                            {

                                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                                // Set the file name to be saved in the Offre model's Photo property
                                string uniqueFileName = $"{timestamp}_{vm.photo.FileName}";

                                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", uniqueFileName);

                                // Ensure the directory exists
                                Directory.CreateDirectory(Path.GetDirectoryName(pathFile));

                                // Supprimer l'ancienne photo s'il en existe une
                                if (!string.IsNullOrEmpty(fournisseur.Photo))
                                {
                                    string oldFilePath = Path.Combine("wwwroot/images", fournisseur.Photo);
                                    if (System.IO.File.Exists(oldFilePath))
                                    {
                                        System.IO.File.Delete(oldFilePath);
                                    }
                                }

                                using (var stream = new FileStream(pathFile, FileMode.Create))
                                {
                                    //fournisseur.Photo = o.Photo;
                                    vm.photo.CopyTo(stream);
                                }

                                fournisseur.Photo = uniqueFileName;


                                HttpContext.Session.SetString("Photo", fournisseur.Photo);
                            }
                            else
                            {
                                // Add an error message to ModelState if the file does not meet the criteria
                                ModelState.AddModelError("photo", "Le fichier doit être une image JPEG, JPG ou PNG et ne doit pas dépasser 100000 octets.");
                                return View(vm);
                            }
                        }
                        if (vm.description != null)
                        {
                            fournisseur.Description = vm.description;
                        }



                            db.SaveChanges();
                        return RedirectToAction("FournisseurHome", "Fournisseur");

                    }

                    db.SaveChanges();
                    return RedirectToAction("Accueil", "Home");
                }

              return NotFound();

            }
            return View(vm);
        }


        public IActionResult ChangeMotPasse()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangeMotPasse(ChangerMotPasseVM vm)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("Id");
                var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();

                byte[] salt = Convert.FromBase64String(user.Salt);

                bool passwordMatch = VerifyPassword(vm.motDePasse, user.MotDePasse, salt);

                if (passwordMatch)
                {
                    string Salt;
                    string hashedPassword = HashPassword(vm.nouveauMotDePasse, out Salt);

                    user.MotDePasse = hashedPassword;
                    user.Salt = Salt;

                    db.Update(user);
                    db.SaveChanges();
                    return RedirectToAction("Accueil", "Home");
                }
                else
                {
                    ModelState.AddModelError("motDePasse", "le mot de passe est incorrect .");
                    return View(vm);
                }
            }
            return View(vm);
        }

        public IActionResult Inscription()
        {
            var viewModel = new InscriptionVM
            {
                // Creating the list of cities
                Cities = db.Villes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),

                // Creating the list of métiers
                Métiers = db.Metiers.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> InscriptionAsync(InscriptionVM vm)
        {
            // If UserType is Client, ignore metierId validation
            if (vm.UserType == InscriptionVM.userType.Client)
            {
                ModelState.Remove("metierId"); // Remove metierId from the validation
            }

            vm.Cities = db.Villes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            vm.Métiers = db.Metiers.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            if (ModelState.IsValid)
            {
                // verifier que le login(email) est unique
                int count = db.Users.Where(us => us.Email == vm.Email).Count();
                if (count == 0)
                {
                    Users user;

                    //email Verification 

                    //// Generate a verification token
                    //var verificationToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

                    //// Store the token in session
                    //HttpContext.Session.SetString("VerificationToken", verificationToken);

                    // Store the token in session and send the email
                    // await _emailService.SendVerificationEmailAsync(vm.Email);
                    string salt;
                    string hashedPassword = HashPassword(vm.MotDePasse, out salt);
                    if (vm.UserType.ToString() == "Client")
                    {
                        user = new Client
                        {
                            Nom = vm.Nom,
                            Prenom = vm.Prenom,
                            CIN = vm.CIN,
                            Telephone = vm.Telephone,
                            Email = vm.Email,
                            Salt = salt,
                            MotDePasse = hashedPassword,
                            villeId = vm.villeId,
                        };
                    }
                    else if (vm.UserType.ToString() == "Fournisseur")
                    {
                        user = new Fournisseur
                        {
                            Nom = vm.Nom,
                            Prenom = vm.Prenom,
                            CIN = vm.CIN,
                            Telephone = vm.Telephone,
                            Email = vm.Email,
                            Salt = salt,
                            Disponibiliter = true,
                            MotDePasse = hashedPassword,
                            villeId = vm.villeId,
                            MetierId = vm.metierId
                        };
                    }
                    else
                    {
                        // Handle error: invalid role
                        ModelState.AddModelError("Role", "Invalid role selected.");
                        return View(vm);
                    }

                    // return RedirectToAction("EmailVerificationSent");
                    db.Users.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("Login", "Authentification");
                }

                ModelState.AddModelError("Email", "Email existe deja ");
            }
            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            var sessionToken = HttpContext.Session.GetString("VerificationToken");


            if (user != null && token == sessionToken)
            {
                //user.IsVerified = true;

                await db.SaveChangesAsync();
                return RedirectToAction("Login", "Authentification");
            }

            return View("EmailVerificationFailed"); // Failure view
        }
        private string HashPassword(string password, out string base64Salt)
        {
            using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                // Generate a secure random salt
                byte[] salt = new byte[16];
                RandomNumberGenerator.Fill(salt); // Use RandomNumberGenerator to fill the salt array

                hasher.Salt = salt;
                hasher.DegreeOfParallelism = 4; // Number of threads to use
                hasher.MemorySize = 1024; // In KB
                hasher.Iterations = 3; // Number of iterations

                byte[] hash = hasher.GetBytes(32); // Hash length
                base64Salt = Convert.ToBase64String(salt); // Return the salt as Base64 string

                return Convert.ToBase64String(hash);
            }
        }

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

        public IActionResult listeUtilisateurs()
        {
            var users = db.Users
           .Where(u => u is Fournisseur || u is Client)
           .Include(u => u.Ville) // Include related Ville data
           .Include(u => (u as Fournisseur).metier) // Ensure Metier is included for Fournisseur           .ToList();
           .ToList();
            return View(users);
        }



        //   public IActionResult modifierProfils()
        //   {
        //       // Récupérez l'utilisateur actuel depuis la base de données
        //       int id = (int)HttpContext.Session.GetInt32("Id");
        //       var user = db.Users.FirstOrDefault(u => u.Id == id);

        //       //Les information de fornisseur .
        //       // var fornisseur = db.Fournisseurs.ToList();
        //       if (user == null)
        //       {
        //           return NotFound();
        //       }
        //       ViewBag.UserType = user.UserType;
        //       // Convertir l'utilisateur en ViewModel pour l'affichage
        //       var vm = new modifierProfilsVm();
        //       {
        //           vm.Nom = user.Nom;
        //           vm.Prenom = user.Prenom;
        //           vm.Email = user.Email;
        //           vm.CIN = user.CIN;
        //           vm.Telephone = user.Telephone;


        //           //code ajouter
        //           if (user.UserType == "Fournisseur")
        //           {
        //               var fournisseur = db.Fournisseurs.ToList().FirstOrDefault(f => f.Id == id);
        //               if (fournisseur != null)
        //               {
        //                   //vm.Photo = fournisseur.Photo;
        //                   //vm.Photo = "data:image/png;base64," + Convert.ToBase64String(fournisseur.Photo);
        //                   //var photo = vm.Photo;
        //                   string photos = fournisseur.Photo;
        //                   vm.photos = photos;
        //               }
        //               // Autres propriétés...
        //           };
        //           return View(vm);
        //       }

        //   }
        //   [HttpPost]
        //   public IActionResult modifierProfils([FromForm] modifierProfilsVm o)
        //   {
        //       // Récupérez l'utilisateur actuel depuis la base de données
        //       int id = (int)HttpContext.Session.GetInt32("Id");
        //       var user = db.Users.FirstOrDefault(u => u.Id == id);
        //       if (ModelState.IsValid)
        //       {
        //           if (user != null)
        //           {
        //               // Mettre à jour les informations du profil
        //               user.Nom = o.Nom;
        //               user.Prenom = o.Prenom;
        //               user.Email = o.Email;
        //               user.CIN = o.CIN;
        //               user.Telephone = o.Telephone;
        //               db.SaveChanges();
        //               // Autres propriétés...
        //               var fournisseur = db.Fournisseurs.FirstOrDefault(f => f.Id == id);
        //               if (user.UserType == "Fournisseur")
        //               {
        //                   //code ajouter
        //                   if (o.Photo != null && o.Photo.Length > 0)
        //                   {
        //                       string[] allowedExtensions = { ".jpg", ".png", ".jpeg" , ".svg"};
        //                       string fileExt = Path.GetExtension(o.Photo.FileName).ToLower();
        //                       if (allowedExtensions.Contains(fileExt))
        //                       {
        //                           string uniqueFileName = $"{DateTime.Now.Ticks}_{Guid.NewGuid().ToString().Substring(0, 4)}{fileExt}"; // Combinaison d'un horodatage et d'un identifiant unique
        //                           string pathFile = Path.Combine("wwwroot/images", uniqueFileName);
        //                           // Supprimer l'ancienne photo s'il en existe une
        //                           if (!string.IsNullOrEmpty(fournisseur.Photo))
        //                           {
        //                               string oldFilePath = Path.Combine("wwwroot/images", fournisseur.Photo);
        //                               if (System.IO.File.Exists(oldFilePath))
        //                               {
        //                                   System.IO.File.Delete(oldFilePath);
        //                               }
        //                           }
        //                           using (var stream = new FileStream(pathFile, FileMode.Create))
        //                           {
        //                               //fournisseur.Photo = o.Photo;
        //                               o.Photo.CopyTo(stream);
        //                           }
        //                           fournisseur.Photo = uniqueFileName;
        //                           db.SaveChanges();
        //                       }
        //                       // stocke photo dans la taable fornisseur :
        //                       HttpContext.Session.SetString("Photo", fournisseur.Photo);
        //                   }
        //                   db.SaveChanges();
        //                   // Mettre à jour les valeurs de session si nécessaire //pour prend la modification a la base donne
        //                   HttpContext.Session.SetString("Nom", user.Nom);
        //                   HttpContext.Session.SetString("Prenom", user.Prenom);
        //                   HttpContext.Session.SetString("Email", user.Email);
        //                   HttpContext.Session.SetString("CIN", user.CIN.ToString());
        //                   HttpContext.Session.SetString("Telephone", user.Telephone.ToString());
        //                   // Redirigez vers une page de confirmation ou affichez un message de succès
        //                   return RedirectToAction("Index", "Accueil");

        //               }
        //           }
        //       }
        //       return RedirectToAction("Index", "Accueil");
        //   }

    }
}