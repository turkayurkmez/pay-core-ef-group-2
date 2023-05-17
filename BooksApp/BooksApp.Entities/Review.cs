namespace BooksApp.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string VoterName { get; set; }
        public string? Comment { get; set; }
        public int? RatingWithStars { get; set; }

        public int BookId { get; set; }
    }
}