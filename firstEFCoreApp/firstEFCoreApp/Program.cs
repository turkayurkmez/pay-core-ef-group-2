// See https://aka.ms/new-console-template for more information
using firstEFCoreApp.Models;

Console.WriteLine("Hello, World!");

Product product = new Product();
product.Category = new Category { Name = "Kozmetik" };
Console.WriteLine(product.Category.Name);

Category laptop = new Category();
laptop.Products.Add(new Product { Name = "Mac book pro" });
