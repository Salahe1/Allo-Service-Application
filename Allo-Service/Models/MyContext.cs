using Microsoft.EntityFrameworkCore;
using System;

namespace Allo_Service.Models
{
    public class MyContext:DbContext
    {
      
        public DbSet<Avis> Avis { get; set; }

        public DbSet<Abonement> Abonementes { get; set; }
        
        public DbSet<Message> Messages { get; set; }

        public DbSet<Metier> Metiers { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Offre> Offres { get; set; }

        public DbSet<Paiement> Paiements { get; set; }

        public DbSet<Reclamation> Reclamations { get; set; }

   
         public DbSet<Users> Users { get; set; }

        public DbSet<Villes> Villes { get; set; }

        //public DbSet<Product> Products { get; set; }
        //public DbSet<Service> Services { get; set; }
        //public DbSet<SimpleUser> SimpleUsers { get; set; }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Fournisseur>("Fournisseur")
                .HasValue<Client>("Client")
                .HasValue<Administrateur>("Administrateur");
        }

        public MyContext(DbContextOptions<MyContext> opt):base(opt) { }
     

    }
  

   
}
