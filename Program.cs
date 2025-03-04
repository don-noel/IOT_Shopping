// Initialisation de l'application
using Microsoft.EntityFrameworkCore;
using IOT_Shopping.Models;
using System;
var builder = WebApplication.CreateBuilder(args);

// Connexion à la base de données
var connectionString = builder.Configuration.GetConnectionString("ConnexionParDefaut");
builder.Services.AddDbContext<ContexteBaseDeDonnees>(options =>
    options.UseSqlServer(connectionString)); // ✅ Vérifie bien que ça utilise SQL Server

// Ajouter des services MVC
builder.Services.AddControllersWithViews();

// Création et configuration de l'application
var app = builder.Build();

// Gestion des erreurs et sécurité
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Ajout des middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Définition des routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Ajout de données de test dans la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ContexteBaseDeDonnees>();

    // Vérifie si la base est vide avant d'ajouter des produits
    if (!context.Produits.Any())
    {
        context.Produits.AddRange(
            new Produit { Nom = "Ordinateur Portable", Prix = 1200, Description = "Un PC performant" },
            new Produit { Nom = "Smartphone", Prix = 800, Description = "Un téléphone moderne" },
            new Produit { Nom = "Tablette", Prix = 500, Description = "Idéal pour le multimédia" }
        );

        context.SaveChanges();
    }
}

// Démarreage de l'application
app.Run();
