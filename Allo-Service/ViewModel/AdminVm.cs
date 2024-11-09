using System.ComponentModel.DataAnnotations;

namespace Allo_Service.ViewModel
{
    public class AdminVm
    {
        public int? Id { get; set; }    

        [Required(ErrorMessage = "Il faut entrer un nom.")]
        [StringLength(20,ErrorMessage =" le nom ne peut pas dépasser 20 caractére.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Il faut entrer un prénom.")]
        [StringLength(20,ErrorMessage ="le prénom ne peut pas dépasser 20 caractére.")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Il faut entrer un CIN.")]
        [RegularExpression(@"^[A-Z]{1,2}[0-9]{6,8}$", ErrorMessage = "Format du CIN invalide.")]
        public string CIN { get; set; }

        [Required(ErrorMessage = "Il faut entrer un Email.")]
        [EmailAddress(ErrorMessage ="le format d'Email est incorrect.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Il faut entrer un num de télèphone.")]
        [Phone(ErrorMessage = "Format du numéro de téléphone invalide.")]
        [RegularExpression(@"^(\+212|0)([ \-_/]*)(\d[ \-_/]*){9}$", ErrorMessage = "Format du numéro de téléphone invalide.")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Il faut choisir un role")]
        public role Role { get; set; }

        [Required(ErrorMessage = "Il faut entrer un mot de passe")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Le mot de passe doit comporter au moins 6 caractères.")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }

        [Required(ErrorMessage = "Il faut confirmer votre mot de passe.")]
        [Compare("MotDePasse", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        [DataType(DataType.Password)]
        public string ConfirmationMotDePasse { get; set; }

        [Required(ErrorMessage = "Il faut choisir une ville.")]
        public int villeId { get; set; }



        public enum role
        {
            SuperAdmin,
            Fournisse
        }
    }
}
