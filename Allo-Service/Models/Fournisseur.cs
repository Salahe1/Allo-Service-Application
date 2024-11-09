

namespace Allo_Service.Models
{
    public class Fournisseur : SimpleUser
    {

        public Boolean? Disponibiliter { get; set; }

        public string Photo { get; set; }
        
        public string Description { get; set; }

        public Metier? metier { get; set; }
        public int? MetierId { get; set; }

        public IList<Offre>? Offres { get; set; }

        public Abonement? Abonnement { get; set; }
        public int? AbonnementId { get; set; }

        public IList<Avis>? Avis { get; set; }

        public IList<Message>? Messages { get; set; }

        public IList<Reclamation>? Reclamations { get; set; }
    }
}