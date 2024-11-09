
namespace Allo_Service.Models
{
    public class Offre
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }    
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public Fournisseur? Fournisseur { get; set; }
        public int?  FournisseurId { get; set; }
    }
}
