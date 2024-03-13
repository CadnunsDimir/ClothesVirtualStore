namespace ClothesVirtualStore.Api.Products.Data.Models;

public record Product (Guid Id, string Name, decimal Price, string? Description, string? ImageUrl);