namespace Allo_Service.Models
{
    public class Metier
	{
		public int Id { get; set; }
		public string Name { get; set; }
        public string? Photo { get; set; }
        public IList<Fournisseur>? Fournisseurs { get; set; }
	}
}




         //public Service? service { get; set; }
        //public int? ServiceId { get; set; }