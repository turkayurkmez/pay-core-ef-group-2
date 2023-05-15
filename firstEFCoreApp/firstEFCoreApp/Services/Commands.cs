using firstEFCoreApp.Data;
using firstEFCoreApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace firstEFCoreApp.Services
{
    public static class Commands
    {
        public static bool CreateDatabaseAndSeed(bool onlyIfNoDb)
        {
            using var db = new CatalogAppDbContext();
            if (onlyIfNoDb && (db.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                return false;
            }

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            if (!db.Products.Any())
            {
                WriteTestData(db);
            }

            return true;
        }

        private static void WriteTestData(this CatalogAppDbContext db)
        {
            var category = new Category { Name = "Bilgisayar" };

            var products = new List<Product>
            {
                new(){ Name ="Dell XPS 13", Category = category, Description="Dell XPS 13", Price=45000},
                new(){ Name ="Dell XPS 15", Category = category, Description="Dell XPS 15", Price=54000},
                new(){ Name ="Lenovo", Category = category, Description="Lenovo Laptop", Price=40000},
                new(){ Name ="Basic Tişört", Category = new Category{ Name="Giyim"}, Description="Tek renk desensi....", Price=150}
            };

            db.Products.AddRange(products);
            db.SaveChanges();


        }

        public static void ListProducts()
        {
            using var db = new CatalogAppDbContext();
            foreach (var product in db.Products.Include(p => p.Category).AsNoTracking())
            {
                Console.WriteLine($"{product.Name} -> {product.Price} ({product.Category.Name})");
            }

        }

        public static void ChangeCategoryName()
        {
            using var db = new CatalogAppDbContext();
            var singleProduct = db.Products.Include(x => x.Category)
                                           .Single(p => p.Name == "Basic Tişört");

            singleProduct.Category.Name = "Yazlık Giyim";
            db.SaveChanges();
            Console.WriteLine("SaveChanges metodu çağrıldı");
            Console.WriteLine("------ Son durum ------");
            ListProducts();
        }
    }
}
