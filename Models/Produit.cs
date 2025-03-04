// Importation des bibliothèques
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Déclaration de la classe Produit
namespace IOT_Shopping.Models
{
    public class Produit
    {
        // Définition des propriétés
        // clé primaire
        [Key]
        public int Id { get; set; }

        // nom du produit
        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        // champ prix
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Prix { get; set; }


        // champ description
        [StringLength(255)]
        public string? Description { get; set; }

        // champ image
        public string? Image { get; set; }

        // champ catégorie
        [Required]
        [StringLength(50)]
        public string Categorie { get; set; }
    }
}
