namespace ClothesVirtualStore.Api.Products.Models;

public class Product
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}