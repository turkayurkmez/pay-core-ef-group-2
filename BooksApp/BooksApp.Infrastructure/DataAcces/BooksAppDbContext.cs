using BooksApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Infrastructure.DataAcces
{
    public class BooksAppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Review> Reviews { get; set; }


        public BooksAppDbContext(DbContextOptions<BooksAppDbContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API
            modelBuilder.Entity<BookAuthor>().HasKey(b => new { b.AuthorId, b.BookId });


            /*
             * Bir kaç kitap ve yazar gireceğiz.
             */
            Tag sciFi = new Tag { TagId = "Bilim-Kurgu" };
            Tag horror = new Tag { TagId = "Korku" };



            modelBuilder.Entity<Tag>().HasData(
                  sciFi, horror
                );

            Author isaac = new Author { AuthorId = 1, Name = "Isaac Asimow" };
            Author stephen = new Author { AuthorId = 2, Name = "Stephen King" };

            BookAuthor bookAuthor = new BookAuthor { AuthorId = 1, BookId = 1, Order = 1 };
            BookAuthor bookAuthor2 = new BookAuthor { AuthorId = 2, BookId = 2, Order = 1 };

            List<BookAuthor> bookAuthors = new() { bookAuthor };
            modelBuilder.Entity<BookAuthor>().HasData(bookAuthors);



            modelBuilder.Entity<Author>().HasData(isaac, stephen);

            Book book1 = new Book
            {
                BookId = 1,
                Title = "Vakıf",
                Description = "Asimov'un muhteşem serisi",
                //Tags = new List<Tag> { sciFi, horror },


            };

            Book book2 = new Book
            {
                BookId = 2,
                Title = "O",
                Description = "Palyaço korkusu yaratan bir kitap",
                //Tags = new List<Tag> { sciFi, horror },


            };

            modelBuilder.Entity<Book>().HasData(book1, book2);

        }

    }
}
