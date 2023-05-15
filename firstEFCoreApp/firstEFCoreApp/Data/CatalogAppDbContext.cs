using firstEFCoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace firstEFCoreApp.Data
{
    public class CatalogAppDbContext : DbContext
    {
        /*
         *  xxxFirst
         *  Code <-> Database
         *    |          |
         *    |        Veritabanı zaten var (önceden yapılmış) ya da DBA takımları tarafından icra ediliyorsa.....   
         *    |
         *  Uygulama sıfırdan geliştiriliyor. YG takımı kendi db'lerinden sorumludur.
         *                
         *  
         */
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(200);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=CatalogDbForGroup2;Integrated Security=True");

        }
    }

}
