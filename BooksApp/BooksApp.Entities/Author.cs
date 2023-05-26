namespace BooksApp.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookAuthor> BooksLink { get; set; }


    }
}