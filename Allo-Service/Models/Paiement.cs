
namespace Allo_Service.Models
{
    public class Paiement
    {
        public int Id { get; set; }
        public float Montant { get; set; }
        public string CartePaiement { get; set; }
        public Abonement? Abonnement { get; set; }
        public int? AbonnementId { get; set; }
    }
}
