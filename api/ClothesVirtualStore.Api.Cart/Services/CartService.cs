using System.Text.Json;
using System.Text.Json.Serialization;
using ClothesVirtualStore.Api.Cart.Services;
using ClothesVirtualStore.CommonsLib.Models;

namespace ClothesVirtualStore.Api.Cart;

public class CartService : ICartService
{
    private IMQProcessingService mQProcessingService;
    private readonly ICachingService<Models.Cart> caching;

    public CartService(ICachingService<Models.Cart> caching, IMQProcessingService mQProcessingService)
    {
        this.mQProcessingService = mQProcessingService;
        this.caching = caching;
    }

    // TODO: implement redis    
    // public Models.Cart? GetCart(Guid sessionId) => cartRepository.FirstOrDefault(x=> x.SessionId == sessionId.ToString());
    public async Task<Models.Cart?> GetCart(string sessionId) {
        return await caching.GetAsync(sessionId);
    }

    public Order Checkout(Models.Cart cart, string cpf)
    {
        var order = new Order(Guid.NewGuid(), cpf, cart.Itens);
        mQProcessingService.EnqueueOrder(order);
        return order;
    }

    public async void UpdateCart(Models.Cart cart)
    {
        await caching.SetAsync(cart.SessionId, cart);
    }

    public async Task<Models.Cart?> CreatOrGetCart(string? sessionIdAsString)
    {
        var cart = await caching.GetAsync(sessionIdAsString ?? "");

        if (cart == null)
        {
            cart = GenerateNew();
            await caching.SetAsync(cart.SessionId, cart);
        }

        return cart;
    }

    private Models.Cart GenerateNew() => new Models.Cart(Guid.NewGuid().ToString(), new List<CartItem>());
}
