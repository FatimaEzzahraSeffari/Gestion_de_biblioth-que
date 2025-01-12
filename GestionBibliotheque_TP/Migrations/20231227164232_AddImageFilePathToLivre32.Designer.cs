﻿// <auto-generated />
using System;
using GestionBibliotheque_TP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionBibliotheque_TP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231227164232_AddImageFilePathToLivre32")]
    partial class AddImageFilePathToLivre32
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestionBibliotheque_TP.Models.Abonne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Abonne");
                });

            modelBuilder.Entity("GestionBibliotheque_TP.Models.Emprunt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AbonneId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEmprunt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRetour")
                        .HasColumnType("datetime2");

                    b.Property<int>("LivreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AbonneId");

                    b.HasIndex("LivreId");

                    b.ToTable("Emprunt");
                });

            modelBuilder.Entity("GestionBibliotheque_TP.Models.Livre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Auteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EstEmprunte")
                        .HasColumnType("bit");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Livre");
                });

            modelBuilder.Entity("GestionBibliotheque_TP.Models.UserRegister", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRegisters");
                });

            modelBuilder.Entity("GestionBibliotheque_TP.Models.Emprunt", b =>
                {
                    b.HasOne("GestionBibliotheque_TP.Models.Abonne", "Abonne")
                        .WithMany("Emprunts")
                        .HasForeignKey("AbonneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionBibliotheque_TP.Models.Livre", "Livre")
                        .WithMany("Emprunts")
                        .HasForeignKey("LivreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Abonne");

                    b.Navigation("Livre");
                });

            modelBuilder.Entity("GestionBibliotheque_TP.Models.Abonne", b =>
                {
                    b.Navigation("Emprunts");
                });

            modelBuilder.Entity("GestionBibliotheque_TP.Models.Livre", b =>
                {
                    b.Navigation("Emprunts");
                });
#pragma warning restore 612, 618
        }
    }
}
