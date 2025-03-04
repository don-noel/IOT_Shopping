using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using IOT_Shopping.Models;
using System.IO;
using System.Linq;

namespace IOT_Shopping.Controllers
{
    public class ProduitController : Controller
    {
        private readonly ContexteBaseDeDonnees _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProduitController(ContexteBaseDeDonnees context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var produits = _context.Produits.ToList();
            return View(produits);
        }

        public IActionResult Create()
        {
            // ✅ Récupérer les catégories distinctes existantes dans la base de données
            ViewBag.Categories = _context.Produits
                                         .Select(p => p.Categorie)
                                         .Distinct()
                                         .ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Produit produit, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    produit.Image = uniqueFileName;
                }

                _context.Produits.Add(produit);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // ✅ S'assurer que la liste des catégories est renvoyée en cas d'erreur
            ViewBag.Categories = _context.Produits
                                         .Select(p => p.Categorie)
                                         .Distinct()
                                         .ToList();

            return View(produit);
        }

        public IActionResult Edit(int id)
        {
            var produit = _context.Produits.Find(id);
            if (produit == null) return NotFound();

            // ✅ Charger les catégories pour la modification
            ViewBag.Categories = _context.Produits
                                         .Select(p => p.Categorie)
                                         .Distinct()
                                         .ToList();

            return View(produit);
        }

        [HttpPost]
        public IActionResult Edit(Produit produit, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Path.GetFileName(imageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    produit.Image = uniqueFileName;
                }

                _context.Produits.Update(produit);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // ✅ Recharger les catégories en cas d'erreur de validation
            ViewBag.Categories = _context.Produits
                                         .Select(p => p.Categorie)
                                         .Distinct()
                                         .ToList();

            return View(produit);
        }

        public IActionResult Delete(int id)
        {
            var produit = _context.Produits.Find(id);
            if (produit == null) return NotFound();
            _context.Produits.Remove(produit);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
