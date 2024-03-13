using ClothesVirtualStore.Api.Products.Data.Models;

namespace ClothesVirtualStore.Api.Products.Data.ViewModels;

public class ProductViewModel
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    public Product ToDbModel()
    {
       return new Product(Guid.NewGuid(), Name, Price, Description, ImageUrl);
    }
}