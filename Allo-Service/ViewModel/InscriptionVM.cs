using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Allo_Service.ViewModel
{
    public class InscriptionVM 
    {
        [Required(ErrorMessage = "Le champ Nom est requis.")]
        [StringLength(50, ErrorMessage = "Le champ Nom ne peut pas dépasser 50 caractères.")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le champ Prénom est requis.")]
        [StringLength(50, ErrorMessage = "Le champ Prénom ne peut pas dépasser 50 caractères.")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le champ CIN est requis.")]
        [RegularExpression("[A-Z]{1,2}[0-9]{5,6}", ErrorMessage = "vous devez respecter la format de CIN.")]
        public string CIN { get; set; }

        [Required(ErrorMessage = "Le champ Telephone est requis.")]
        //[RegularExpression("[0-9]{10}", ErrorMessage = "Le champ Téléphone doit être un nombre à 10 chiffres.")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Le champ Email est requis.")]
        [EmailAddress(ErrorMessage = "Le champ Email n'est pas une adresse email valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champ Rôle est requis.")]
        public userType UserType { get; set; }

        [Required(ErrorMessage = "Le champ Mot de Passe est requis.")]
        [DataType(DataType.Password)]
        public string MotDePasse { get; set; }

        [Compare("MotDePasse", ErrorMessage = "Le champ Confirmation Mot de Passe doit correspondre au Mot de Passe.")]
        [DataType(DataType.Password)]
        public string ConfirmMotDePasse { get; set; }

        [Required(ErrorMessage = "Le champ Ville est requis.")]
        public int villeId { get; set; }

        [Required(ErrorMessage = "Le champ Métier est requis.")]
        public int metierId { get; set; }


        public IEnumerable<SelectListItem>? Cities { get; set; }
        public List<SelectListItem>? Métiers { get; set; }

        public enum userType
        {
            Client,
            Fournisseur
        }

    }
}
