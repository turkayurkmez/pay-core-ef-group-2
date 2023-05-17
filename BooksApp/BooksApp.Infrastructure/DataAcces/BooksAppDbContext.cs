using BooksApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Infrastructure.DataAcces
{
    public class BooksAppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<PriceOffer> PriceOffers { get; set; }
        public List<Tag> Tags { get; set; }

        public BooksAppDbContext(DbContextOptions<BooksAppDbContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
            modelBuilder.Entity<BookAuthor>().HasData(bookAuthor);


            List<BookAuthor> bookAuthors = new() { bookAuthor };
            modelBuilder.Entity<Author>().HasData(isaac, stephen);

            Book book1 = new Book
            {
                BookId = 1,
                Title = "Test",
                Description = "Teest"


            };

            modelBuilder.Entity<Book>().HasData(book1);

        }

    }
}
