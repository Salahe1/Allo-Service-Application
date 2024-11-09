
namespace Allo_Service.Models
{
    public class  Users
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string CIN { get; set; }
        public string Telephone { get;  set; }
        public string Email { get;  set; }
        public string MotDePasse { get; set; }
        //public string UserType { get; set; }
        public Villes? Ville { get; set; }
        public int? villeId { get; set; }
        public string Salt { get; internal set; }

       // public bool IsVerified { get; set; } = false;
    }
}
