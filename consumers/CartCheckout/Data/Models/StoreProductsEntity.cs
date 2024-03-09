namespace ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;

public class StoreProductsEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}