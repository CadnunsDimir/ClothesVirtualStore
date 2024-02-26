using ClothesVirtualStore.Api.Cart.Models;
using ClothesVirtualStore.Api.Cart.Services;
using ClothesVirtualStore.CommonsLib.Models;

namespace ClothesVirtualStore.Api.Cart;

public class CartService : ICartService
{
    private IMQProcessingService mQProcessingService;

    public CartService(IMQProcessingService mQProcessingService)
    {
        this.mQProcessingService = mQProcessingService;
    }
    public Models.Cart GetCart(string sessionId) => new Models.Cart(
        sessionId,
        new List<CartItem> {
                new CartItem {
                    Amount = 2,
                    ProductId = Guid.Parse("a5e7c482-49cc-4315-b360-3c6c02feb461")
                }
            }
    );
    
     public Order Checkout(Models.Cart cart, string cpf)
    {
        var order = new Order(Guid.NewGuid(), cpf, cart.Itens);
        mQProcessingService.EnqueueOrder(order);
        return order;
    }
}
