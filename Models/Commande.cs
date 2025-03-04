// Importation des bibliothèques
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Déclaration de la classe Commande
namespace IOT_Shopping.Models
{
    public class Commande
    {
        // Clé primaire
        [Key]
        public int Id { get; set; }

        // ProduitId  (Identifiant du produit de commande)
        [Required(ErrorMessage = "Le produit est obligatoire.")]
        [Range(1, int.MaxValue, ErrorMessage = "Veuillez sélectionner un produit valide.")]
        public int ProduitId { get; set; }

        // Produit (Relation avec la table Produits)
        [ForeignKey("ProduitId")]
        public Produit? Produit { get; set; } 

        // Quantite 
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être supérieure à 0.")]
        public int Quantite { get; set; }

        // Nom client, Prenom Client, Adresse Client, Telephone Client, Date Naissance Client, Date Commande
        [Required]
        public string NomClient { get; set; } = string.Empty; 

        [Required]
        public string PrenomClient { get; set; } = string.Empty; 

        [Required]
        public string AdresseClient { get; set; } = string.Empty; 

        [Required]
        [Phone(ErrorMessage = "Veuillez entrer un numéro de téléphone valide.")] 
        public string TelephoneClient { get; set; } = string.Empty; 

        [Required]
        public DateTime DateNaissanceClient { get; set; }

        [Required]
        public DateTime DateCommande { get; set; } = DateTime.Now;

        [NotMapped]
        public decimal Total => Produit != null ? Quantite * Produit.Prix : 0;
    }
}
