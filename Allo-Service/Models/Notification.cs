using System.ComponentModel.DataAnnotations.Schema;

namespace Allo_Service.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateNotif { get; set; }
        public string Contenu { get; set; }
        public string Titre { get; set; }
        public string UserType { get; set; }
        public Administrateur? Administrateur { get; set; }
        [ForeignKey("Administrateur")]
        public int? AdministrateurId {  get; set; }


    }

}
