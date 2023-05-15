namespace firstEFCoreApp.Models
{
    public class Product
    {
        //POCO: Plain Old C# Object -> Bildiğiniz C# Nesnesi
        //POJO: Plain Old Java Object -> 


        public int Id { get; set; } //Convention over Configuration (isme göre konfigürasyon): Id ile bitiyorsa; PK -> identity(1,1)
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

        public int CategoryId { get; set; }

        //Navigation Property: İlişkili olduğu entity'ye erişmek (navigasyon) için kullanılan özellik:
        public Category Category { get; set; }
    }
}
