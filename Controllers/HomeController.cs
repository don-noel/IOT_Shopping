using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IOT_Shopping.Models;
using System.Linq;

namespace IOT_Shopping.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContexteBaseDeDonnees _context;

        public HomeController(ILogger<HomeController> logger, ContexteBaseDeDonnees context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Récupérer toutes les catégories distinctes des produits
            ViewBag.Categories = _context.Produits
                .Select(p => p.Categorie)
                .Distinct()
                .ToList();

            // Récupérer tous les produits
            ViewBag.Produits = _context.Produits.OrderByDescending(p => p.Id).ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
