// Importation des bibliothèques
using Microsoft.AspNetCore.Mvc;
using IOT_Shopping.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// Déclaration du contrôleur
namespace IOT_Shopping.Controllers
{
    public class CommandeController : Controller
    {
        // Déclaration de la base de données
        private readonly ContexteBaseDeDonnees _context;

        public CommandeController(ContexteBaseDeDonnees context)
        {
            _context = context;
        }

        // Afficher toutes les commandes (Méthode Index)
        public IActionResult Index()
        {
            var commandes = _context.Commandes.Include(c => c.Produit).ToList();
            return View(commandes);
        }

        // Afficher le formulaire de commande (Méthode Create)
        public IActionResult Create()
        {
            ViewBag.Produits = _context.Produits.ToList();
            return View(new Commande());
        }

        // Soumettre une commande (Méthode Create [HttpPost])
        [HttpPost]
        public IActionResult Create(Commande commande)
        {
            Console.WriteLine("=== Début de la soumission ===");
            Console.WriteLine($"ProduitId reçu : {commande.ProduitId}");
            Console.WriteLine($"NomClient reçu : {commande.NomClient}");
            Console.WriteLine($"Quantité reçue : {commande.Quantite}");

            // Vérification de la validation
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Validation échouée !");
                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        Console.WriteLine($"Erreur sur {key} : {error.ErrorMessage}");
                    }
                }
                ViewBag.Produits = _context.Produits.ToList();
                return View(commande);
            }

            // Vérifier si le produit existe
            var produit = _context.Produits.FirstOrDefault(p => p.Id == commande.ProduitId);
            if (produit == null)
            {
                Console.WriteLine("Produit introuvable !");
                ModelState.AddModelError("ProduitId", "Le produit sélectionné est invalide.");
                ViewBag.Produits = _context.Produits.ToList();
                return View(commande);
            }

            // Enregistrement de la commande
            commande.Produit = produit;
            commande.DateCommande = DateTime.Now;

            _context.Commandes.Add(commande);
            _context.SaveChanges();

            // Redirection vers la confirmation
            Console.WriteLine($"Commande enregistrée avec ID : {commande.Id}");

            return RedirectToAction("Confirmation", new { id = commande.Id });
        }

        // Page de confirmation d'une commande
        public IActionResult Confirmation(int id)
        {
            Console.WriteLine($"Tentative d'affichage de la commande avec ID : {id}");

            var commande = _context.Commandes
                .Include(c => c.Produit)
                .FirstOrDefault(c => c.Id == id);

            if (commande == null)
            {
                Console.WriteLine("Commande non trouvée !");
                return NotFound("La commande demandée est introuvable.");
            }

            return View(commande);
        }
    }
}
