namespace BooksApp.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
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

        public Book()
        {
            Tags = new HashSet<Tag>();
            AuthorsLink = new HashSet<BookAuthor>();
            Reviews = new HashSet<Review>();

        }




    }
}
