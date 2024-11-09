
namespace Allo_Service.Models
{
    public class Abonement
    {
        public int Id { get; set; }
        public DateTime DateDebut { get; set; }
        public string TypeAbonnement { get; set; }
        public IList<Fournisseur>?  Fournisseurs { get; set; }
        public Paiement? Paiement { get; set; }
    }
}
