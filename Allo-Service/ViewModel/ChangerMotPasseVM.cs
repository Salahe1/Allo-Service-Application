using System.ComponentModel.DataAnnotations;

namespace Allo_Service.ViewModel
{
    public class ChangerMotPasseVM
    {
        [Required(ErrorMessage ="Ce champ est requis.")]
        [DataType(DataType.Password)]
        public string motDePasse { get; set; }

        [Required(ErrorMessage =" Ce champ est requis .")]
        [DataType(DataType.Password)]
        public string nouveauMotDePasse { get; set; }

        [Required(ErrorMessage ="Ce champ est Requis .")]
        [DataType(DataType.Password)]
        [Compare("nouveauMotDePasse",ErrorMessage = "Le champ Confirmation Mot de Passe doit correspondre au Mot de Passe.")]     
        public string confirmNouveauMotDePasse { get; set; }
    }
}
