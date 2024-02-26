namespace ClothesVirtualStore.Api.Cart.Models;
public class CartItem
{
    public int Amount { get; set; }
    public Guid ProductId { get; set; }
}