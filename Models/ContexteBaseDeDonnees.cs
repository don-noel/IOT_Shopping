// Importation des bibliothèques
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

// Définition du contexte de base de données
namespace IOT_Shopping.Models
{
    public class ContexteBaseDeDonnees : DbContext
    {
        // Constructeur de la classe
        public ContexteBaseDeDonnees(DbContextOptions<ContexteBaseDeDonnees> options) : base(options)
        {
        }

        // Définition des tables
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Commande> Commandes { get; set; }

        // Configuration de la base de données
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("ConnexionParDefaut"));
            }
        }
    }
}
