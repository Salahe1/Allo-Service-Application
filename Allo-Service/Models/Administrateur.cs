
namespace Allo_Service.Models
{
    public class Administrateur : Users
    {
        //[Key]
        //public int Id { get; set; }

        public string Role { get; set; }
        //public IList<Notification>? Notifications { get; set; }
        public IList<Reclamation>? Reclamations { get; set; }
    }
}
