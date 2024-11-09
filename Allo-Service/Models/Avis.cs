
namespace Allo_Service.Models
{
    public class Avis
    {
        public int Id { get; set; }
        public int Note { get; set; }
        public DateTime DateHeures { get; set; }
        public Client? Client { get; set; }
        public int?  ClientId { get; set; }
        public Fournisseur? Fournisseur { get; set; }
        public int?  FournisseurId { get; set; }
    }
}
