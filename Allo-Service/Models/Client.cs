

namespace Allo_Service.Models
{
    public class Client : SimpleUser
    {

        //public string Localisation { get; set; }
        //public IList<Client_Service>? Client_Services { get; set; }
        public IList<Reclamation>? Reclamations { get; set; }
        public IList<Message>?  Messages { get; set; }
        public IList<Avis>? Avis { get; set; }
    }
}
