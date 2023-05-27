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
                                             .Include(b => b.AuthorsLink)
                                             .ThenInclude(al => al.Author)
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

        public object GetBooksForPerformance()
        {
            //Single <-> Split
            //Birleştimek için SingleQuery
            //Ayırmak için SplitQuery()
            var books = booksAppDbContext.Books
                                         .Include(b => b.Reviews)
                                         .AsSingleQuery()
                                         .Include(b => b.Tags)
                                         .AsSingleQuery()
                                         .ToList();
            return books;
        }

        public object JoinWithLinq()
        {
            //var query = from book in booksAppDbContext.Set<Book>() //1. koleksiyon
            //            join review in booksAppDbContext.Set<Review>() //2. koleksiyon
            //            on book.BookId equals review.BookId //eşleşme
            //            select new { book.Title, review.Comment }; //belleğe atılacak sütunlar

            //var query = booksAppDbContext.Books.Join(
            //       booksAppDbContext.Reviews,
            //       book => book.BookId,
            //       review => review.BookId,
            //       (book, review) => new
            //       {
            //           book.Title,
            //           review.Comment
            //       }

            //    );

            /*
             *     
             *     
             *           SELECT 
                            Title, Count(Reviews.ReviewId) as TotalReviewsCount
                            FROM Books JOIN Reviews
                            ON Books.BookId = Reviews.BookId  
                         GROUP By Title
             *     
             *     
             */

            var query = from book in booksAppDbContext.Set<Book>()
                        join reviews in booksAppDbContext.Set<Review>()
                           on book.BookId equals reviews.BookId into grouping
                        select new { Title = book.Title, Count = grouping.Count() };

            return query.ToList();
        }

        public object GetBooksForYears()
        {
            //var query = from book in booksAppDbContext.Set<Book>()
            //            group book by book.PublishedDate.Value.Year
            //            into grp
            //            select new { grp.Key, Count = grp.Count() };

            var query = booksAppDbContext.Books.GroupBy(

                  keySelector: gr => gr.PublishedDate.Value.Year,
                  elementSelector: book => new { book.Title, book.Price },
                  resultSelector: (key, collection) => new { key, BooksCount = collection.Count(), MinPrice = collection.Min(b => b.Price) }
                );


            return query;
        }

        public object GetBooksPage(int page)
        {

            int pageSize = 2;
            var result = booksAppDbContext.Books.OrderBy(b => b.BookId)
                                                .Skip((page - 1) * pageSize)
                                                .Take(pageSize);

            return result;
        }

        public object GetAllBooks()
        {
            return booksAppDbContext.Books.IgnoreQueryFilters().ToList();
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
