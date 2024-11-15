﻿// <auto-generated />
using System;
using Allo_Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Allo_Service.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240923152443_addSaltToUsers")]
    partial class addSaltToUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Allo_Service.Models.Abonement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<string>("TypeAbonnement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Abonementes");
                });

            modelBuilder.Entity("Allo_Service.Models.Avis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateHeures")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FournisseurId")
                        .HasColumnType("int");

                    b.Property<int>("Note")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("FournisseurId");

                    b.ToTable("Avis");
                });

            modelBuilder.Entity("Allo_Service.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateHeures")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FournisseurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("FournisseurId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Allo_Service.Models.Metier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Metiers");
                });

            modelBuilder.Entity("Allo_Service.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AdministrateurId")
                        .HasColumnType("int");

                    b.Property<string>("Contenu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateNotif")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdministrateurId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Allo_Service.Models.Offre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FournisseurId")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FournisseurId");

                    b.ToTable("Offres");
                });

            modelBuilder.Entity("Allo_Service.Models.Paiement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AbonnementId")
                        .HasColumnType("int");

                    b.Property<string>("CartePaiement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Montant")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AbonnementId")
                        .IsUnique()
                        .HasFilter("[AbonnementId] IS NOT NULL");

                    b.ToTable("Paiements");
                });

            modelBuilder.Entity("Allo_Service.Models.Reclamation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AdministrateurId")
                        .HasColumnType("int");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FournisseurId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdministrateurId");

                    b.HasIndex("ClientId");

                    b.HasIndex("FournisseurId");

                    b.ToTable("Reclamations");
                });

            modelBuilder.Entity("Allo_Service.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotDePasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("villeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("villeId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("Users");
                });

            modelBuilder.Entity("Allo_Service.Models.Villes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Villes");
                });

            modelBuilder.Entity("Allo_Service.Models.Administrateur", b =>
                {
                    b.HasBaseType("Allo_Service.Models.Users");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Administrateur");
                });

            modelBuilder.Entity("Allo_Service.Models.Client", b =>
                {
                    b.HasBaseType("Allo_Service.Models.Users");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("Allo_Service.Models.Fournisseur", b =>
                {
                    b.HasBaseType("Allo_Service.Models.Users");

                    b.Property<int?>("AbonnementId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disponibiliter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MetierId")
                        .HasColumnType("int");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("AbonnementId");

                    b.HasIndex("MetierId");

                    b.HasDiscriminator().HasValue("Fournisseur");
                });

            modelBuilder.Entity("Allo_Service.Models.Avis", b =>
                {
                    b.HasOne("Allo_Service.Models.Client", "Client")
                        .WithMany("Avis")
                        .HasForeignKey("ClientId");

                    b.HasOne("Allo_Service.Models.Fournisseur", "Fournisseur")
                        .WithMany("Avis")
                        .HasForeignKey("FournisseurId");

                    b.Navigation("Client");

                    b.Navigation("Fournisseur");
                });

            modelBuilder.Entity("Allo_Service.Models.Message", b =>
                {
                    b.HasOne("Allo_Service.Models.Client", "Client")
                        .WithMany("Messages")
                        .HasForeignKey("ClientId");

                    b.HasOne("Allo_Service.Models.Fournisseur", "Fournisseur")
                        .WithMany("Messages")
                        .HasForeignKey("FournisseurId");

                    b.Navigation("Client");

                    b.Navigation("Fournisseur");
                });

            modelBuilder.Entity("Allo_Service.Models.Notification", b =>
                {
                    b.HasOne("Allo_Service.Models.Administrateur", "Administrateur")
                        .WithMany()
                        .HasForeignKey("AdministrateurId");

                    b.Navigation("Administrateur");
                });

            modelBuilder.Entity("Allo_Service.Models.Offre", b =>
                {
                    b.HasOne("Allo_Service.Models.Fournisseur", "Fournisseur")
                        .WithMany("Offres")
                        .HasForeignKey("FournisseurId");

                    b.Navigation("Fournisseur");
                });

            modelBuilder.Entity("Allo_Service.Models.Paiement", b =>
                {
                    b.HasOne("Allo_Service.Models.Abonement", "Abonnement")
                        .WithOne("Paiement")
                        .HasForeignKey("Allo_Service.Models.Paiement", "AbonnementId");

                    b.Navigation("Abonnement");
                });

            modelBuilder.Entity("Allo_Service.Models.Reclamation", b =>
                {
                    b.HasOne("Allo_Service.Models.Administrateur", "Administrateur")
                        .WithMany("Reclamations")
                        .HasForeignKey("AdministrateurId");

                    b.HasOne("Allo_Service.Models.Client", "Client")
                        .WithMany("Reclamations")
                        .HasForeignKey("ClientId");

                    b.HasOne("Allo_Service.Models.Fournisseur", "Fournisseur")
                        .WithMany("Reclamations")
                        .HasForeignKey("FournisseurId");

                    b.Navigation("Administrateur");

                    b.Navigation("Client");

                    b.Navigation("Fournisseur");
                });

            modelBuilder.Entity("Allo_Service.Models.Users", b =>
                {
                    b.HasOne("Allo_Service.Models.Villes", "Ville")
                        .WithMany("users")
                        .HasForeignKey("villeId");

                    b.Navigation("Ville");
                });

            modelBuilder.Entity("Allo_Service.Models.Fournisseur", b =>
                {
                    b.HasOne("Allo_Service.Models.Abonement", "Abonnement")
                        .WithMany("Fournisseurs")
                        .HasForeignKey("AbonnementId");

                    b.HasOne("Allo_Service.Models.Metier", "metier")
                        .WithMany("Fournisseurs")
                        .HasForeignKey("MetierId");

                    b.Navigation("Abonnement");

                    b.Navigation("metier");
                });

            modelBuilder.Entity("Allo_Service.Models.Abonement", b =>
                {
                    b.Navigation("Fournisseurs");

                    b.Navigation("Paiement");
                });

            modelBuilder.Entity("Allo_Service.Models.Metier", b =>
                {
                    b.Navigation("Fournisseurs");
                });

            modelBuilder.Entity("Allo_Service.Models.Villes", b =>
                {
                    b.Navigation("users");
                });

            modelBuilder.Entity("Allo_Service.Models.Administrateur", b =>
                {
                    b.Navigation("Reclamations");
                });

            modelBuilder.Entity("Allo_Service.Models.Client", b =>
                {
                    b.Navigation("Avis");

                    b.Navigation("Messages");

                    b.Navigation("Reclamations");
                });

            modelBuilder.Entity("Allo_Service.Models.Fournisseur", b =>
                {
                    b.Navigation("Avis");

                    b.Navigation("Messages");

                    b.Navigation("Offres");

                    b.Navigation("Reclamations");
                });
#pragma warning restore 612, 618
        }
    }
}
