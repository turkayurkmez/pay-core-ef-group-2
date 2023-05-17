namespace BooksApp.Entities
{
    public class PriceOffer
    {
        public int PriceOfferId { get; set; }
        public decimal NewPrice { get; set; }
        public string AdText { get; set; }

        public int BookId { get; set; }

    }
}