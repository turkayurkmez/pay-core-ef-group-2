using BooksApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly QueryService queryService;

        public QueriesController(QueryService queryService)
        {
            this.queryService = queryService;
        }

        [HttpGet("[action]")]
        public IActionResult GetBooks()
        {
            var books = queryService.GetBooks();
            return Ok(books);
        }

        [HttpGet("[action]")]
        public IActionResult GetBooksWithRelatedData()
        {
            var books = queryService.GetBooksWithRelatedData();
            return Ok(books);
        }
        [HttpGet("[action]")]
        public IActionResult GetBooksWithTag()
        {
            var books = queryService.GetBooksWithSpesicicTag();
            return Ok(books);
        }

        [HttpGet("[action]")]
        public IActionResult GetFirstBook()
        {
            var book = queryService.GetDetailsOfFirstBook();
            return Ok(book);
        }
        [HttpGet("[action]")]
        public IActionResult GetBooksWithLazy()
        {
            var book = queryService.GetBooksWithLazy();
            return Ok(book);
        }

        [HttpGet("[action]")]
        public IActionResult GetBookWithAuthor()
        {
            var book = queryService.GetBookWithAuthor();
            return Ok(book);
        }


        [HttpGet("[action]")]
        public IActionResult GetResultWithSQLQuery()
        {
            var book = queryService.GetTagsAndBooksCount();
            return Ok(book);
        }

        [HttpGet("[action]")]
        public IActionResult GetResultWithLinq()
        {
            var book = queryService.GetTagsAndBooksCountWithLinq();
            return Ok(book);
        }

        [HttpGet("[action]")]
        public IActionResult GetResultWitQueryable()
        {
            var books = queryService.GetBooksWithQueryable(b => b.Title.Contains("Robot") && string.IsNullOrEmpty(b.Publisher));
            var ordered = books.OrderBy(b => b.Price);

            return Ok(ordered.ToList());
        }

        [HttpPost("[action]")]
        public IActionResult ChangeBookPrice(int id, decimal newPrice)
        {
            var book = queryService.ChangeBookPrice(id, newPrice);


            return Ok(book);
        }

        [HttpPost("[action]")]
        public IActionResult AddReviewToBook(int id, string comment, int rating)
        {
            var book = queryService.CreateNewBookWithReview(id, comment, rating);


            return Ok(book);
        }
        [HttpGet("[action]")]
        public IActionResult GetBooksForPerformance()
        {
            var books = queryService.GetBooksForPerformance();
            return Ok(books);
        }

        [HttpGet("[action]")]
        public IActionResult JoinWithLinq()
        {
            var books = queryService.JoinWithLinq();
            return Ok(books);
        }

        [HttpGet("[action]")]
        public IActionResult GetBooksForYears()
        {
            var books = queryService.GetBooksForYears();
            return Ok(books);
        }

        [HttpGet("[action]/{pageNo}")]
        public IActionResult GetBooksForPage(int pageNo)
        {
            var books = queryService.GetBooksPage(pageNo);
            return Ok(books);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllBooks()
        {
            var books = queryService.GetAllBooks();
            return Ok(books);
        }
    }
}
