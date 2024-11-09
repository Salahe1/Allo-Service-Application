namespace Allo_Service.Models
{
    public class Reclamation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Client? Client { get; set; }
        public int? ClientId { get; set; }
        public Fournisseur? Fournisseur { get; set; }
        public int? FournisseurId { get; set; }
       public Administrateur? Administrateur { get; set; }
       public int?  AdministrateurId { get; set; }
    }
}
