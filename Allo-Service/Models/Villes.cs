namespace Allo_Service.Models
{
    public class Villes
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public IList<Users>? users { get; set; }
    }
}
