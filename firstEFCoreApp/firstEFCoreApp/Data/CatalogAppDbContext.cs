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

    }

}
