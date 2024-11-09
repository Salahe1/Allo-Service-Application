using System.ComponentModel.DataAnnotations;

namespace Allo_Service.ViewModel
{
    public class NotificationVM
    {
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Contenu { get; set; }
        [Required]
        public string UserType { get; set; }
    }
}
