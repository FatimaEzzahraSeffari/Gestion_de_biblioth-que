using Microsoft.EntityFrameworkCore;
namespace GestionBibliotheque_TP.Models
{

        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
            public DbSet<Livre> Livre { get; set; }
            public DbSet<Abonne> Abonne { get; set; }
            public DbSet<Emprunt> Emprunt { get; set; }

        public DbSet<UserRegister> UserRegisters { get; set; } 
        public DbSet<Comment> Comment { get; set; }
        public List<Livre> GetLivresEmpruntes(int abonneId)
            {

            
            var livresEmpruntes = from emprunt in Emprunt
                                      where emprunt.AbonneId == abonneId
                                      select emprunt.Livre;

                Console.WriteLine($"Nombre de livres empruntés : {livresEmpruntes.Count()}");

                return livresEmpruntes.ToList();
            }


        }
    }
