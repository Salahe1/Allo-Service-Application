

namespace Allo_Service.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateHeures { get; set; }
        public Client?  Client { get; set; }
        public int?  ClientId  { get; set; }
        public Fournisseur?  Fournisseur { get; set; }
        public int?  FournisseurId { get; set; }
    }
}