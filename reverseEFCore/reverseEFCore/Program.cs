using reverseEFCore.Models.Data;

namespace reverseEFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NorthwindContext context = new NorthwindContext();
            context.Products.ToList().ForEach(p =>
            {
                Console.WriteLine($"{p.ProductName} --> {p.UnitPrice}");
            });
        }
    }
}