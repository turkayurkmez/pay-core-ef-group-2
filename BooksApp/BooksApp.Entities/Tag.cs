using System.ComponentModel.DataAnnotations;

namespace BooksApp.Entities
{
    public class Tag
    {
        [Key]
        [Required]
        [MaxLength(40)]
        public string TagId { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}