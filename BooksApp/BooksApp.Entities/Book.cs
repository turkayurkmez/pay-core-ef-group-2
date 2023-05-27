using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksApp.Entities
{
    public enum BookType
    {
        Ebook = 0,
        PressedBook = 1
    }
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(500)]

        public string? Description { get; set; }

        public DateTime? PublishedDate { get; set; }
        public string? Publisher { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }

        public bool SoftDeleted { get; set; }

        public virtual ICollection<Tag>? Tags { get; set; }

        public virtual ICollection<BookAuthor>? AuthorsLink { get; set; }

        public virtual PriceOffer? Promotion { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public BookType BookType { get; set; } = Entities.BookType.PressedBook;

        public Book()
        {
            Tags = new HashSet<Tag>();
            AuthorsLink = new HashSet<BookAuthor>();
            Reviews = new HashSet<Review>();

        }




    }
}
