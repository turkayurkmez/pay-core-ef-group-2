using BooksApp.Entities;
using BooksApp.Infrastructure.DataAcces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BooksApp.API.Services
{
    public class QueryService
    {
        private readonly BooksAppDbContext booksAppDbContext;

        public QueryService(BooksAppDbContext booksAppDbContext)
        {
            this.booksAppDbContext = booksAppDbContext;
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = booksAppDbContext.Books
                                         .AsNoTracking()
                                         .AsEnumerable();
            return books;

        }

        public IEnumerable<Book> GetBooksWithRelatedData()
        {
            var books = booksAppDbContext.Books.AsNoTracking()
                                               .Include(b => b.Tags)
                                               .Include(b => b.AuthorsLink)
                                                  .ThenInclude(ba => ba.Author)
                                               .Include(b => b.Reviews)
                                               .AsEnumerable();
            return books;




        }

        public IEnumerable<Book> GetBooksWithSpesicicTag()
        {
            //Include kısmında; sadece TagId'si Bilim kurgu olanların tagleri gelsin.
            //Sadece tag'i Bilimkurgu ise joinle:
            //Filtre edilmiş include
            var books = booksAppDbContext.Books.AsNoTracking()
                                           .Include(b => b.Tags
                                                           .Where(t => t.TagId == "Bilim-Kurgu")
                                                   )
                                           .AsEnumerable();
            return books;
        }

        public Book GetDetailsOfFirstBook()
        {
            var firstBook = booksAppDbContext.Books.Single(b => b.BookId == 1);

            booksAppDbContext.Entry(firstBook)
                             .Collection(b => b.Tags)
                             .Load();

            booksAppDbContext.Entry(firstBook)
                             .Collection(b => b.AuthorsLink)
                             .Load();

            booksAppDbContext.Entry(firstBook)
                             .Reference(b => b.Promotion)
                             .Load();

            return firstBook;

        }

        public IEnumerable<Book> GetBooksWithLazy()
        {
            return booksAppDbContext.Books.AsEnumerable();

        }

        public IEnumerable<Book> GetBooksWithQuery()
        {
            var books = booksAppDbContext.Books.FromSql($"SELECT * FROM Books");
            return books;
        }

        public FirstBookDto GetBookWithAuthor()
        {
            var firstBook = booksAppDbContext.Books
                                             .Where(b => b.BookId == 5)
                                             .Select(b => new FirstBookDto
                                             {
                                                 BookId = b.BookId,
                                                 Title = b.Title,
                                                 Authors = getAuthorsInBook(b)

                                             }).Single();

            return firstBook;


        }

        private static string getAuthorsInBook(Book b)
        {
            return string.Join(',', b.AuthorsLink.OrderBy(ba => ba.Order)
                                                 .Select(ba => ba.Author.Name));
        }

        public object GetTagsAndBooksCount()
        {
            var result = booksAppDbContext.Tags.FromSql($"SELECT TagId, Count(Books.BookId) FROM Tags JOIN BookTag ON Tags.TagId = BookTag.TagsTagId JOIN Books ON Books.BookId = BookTag.BooksBookId GROUP BY TagId");

            return result;
        }

        public List<TagsAndBooksDto> GetTagsAndBooksCountWithLinq()
        {
            var result = booksAppDbContext.Tags.Select(b => new TagsAndBooksDto
            {
                TagId = b.TagId,
                Count = b.Books.Count
            }).ToList();

            return result;
        }

        public IQueryable<Book> GetBooksWithQueryable(Expression<Func<Book, bool>> expression)
        {
            var books = booksAppDbContext.Books.AsNoTracking().Where(expression).AsQueryable();
            return books;
        }

        public Book CreateNewBookWithReview(int id, string comment, int rating)
        {
            var book = booksAppDbContext.Books.FirstOrDefault(b => b.BookId == id);
            //var book = new Book
            //{
            //    Description = "Açıklama",
            //    Title = title,
            //    Reviews = new List<Review> { new() { Comment = comment, RatingWithStars = rating } }
            //};
            var review = new Review { Comment = comment, RatingWithStars = rating, VoterName = "testUser" };
            book.Reviews.Add(review);
            book.Description = "Bu veri güncellendi";
            //booksAppDbContext.Books.Add(book);
            var affectedRow = booksAppDbContext.SaveChanges();
            return book;
        }

        public Book ChangeBookPrice(int id, decimal newPrice)
        {
            var book = booksAppDbContext.Books.FirstOrDefault(p => p.BookId == id);
            book.Price = newPrice;
            //booksAppDbContext.Entry(book).State = EntityState.Detached;
            booksAppDbContext.SaveChanges();
            //Aslında ne yapıyor?



            return book;
        }

    }

    public class FirstBookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
    }

    public class TagsAndBooksDto
    {
        public string TagId { get; set; }
        public int Count { get; set; }
    }

}
