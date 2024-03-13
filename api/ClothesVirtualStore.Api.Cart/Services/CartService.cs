using System.Text.Json;
using System.Text.Json.Serialization;
using ClothesVirtualStore.Api.Cart.Services;
using ClothesVirtualStore.CommonsLib.Models;

namespace ClothesVirtualStore.Api.Cart;

public class CartService : ICartService
{
    private IMQProcessingService mQProcessingService;
    private readonly ICachingService caching;
    private List<Models.Cart> cartRepository = new List<Models.Cart>();

    public CartService(ICachingService caching, IMQProcessingService mQProcessingService)
    {
        this.mQProcessingService = mQProcessingService;
        this.caching = caching;
    }

    // TODO: implement redis    
    // public Models.Cart? GetCart(Guid sessionId) => cartRepository.FirstOrDefault(x=> x.SessionId == sessionId.ToString());
    public async Task<Models.Cart?> GetCart(string sessionId) {
        var cartCache = await caching.GetAsync(sessionId);
        if (!string.IsNullOrWhiteSpace(cartCache))
        {
            return JsonSerializer.Deserialize<Models.Cart>(cartCache);
        }
        return null;
    }

    public Order Checkout(Models.Cart cart, string cpf)
    {
        var order = new Order(Guid.NewGuid(), cpf, cart.Itens);
        mQProcessingService.EnqueueOrder(order);
        return order;
    }

    public async void UpdateCart(Models.Cart cart)
    {
        await caching.SetAsync(cart.SessionId, JsonSerializer.Serialize(cart));
    }

    public Models.Cart CreatOrGetCart(string? sessionIdAsString)
    {
        Guid sessionId;
        Models.Cart? cart = null;
        var isGuid = Guid.TryParse(sessionIdAsString, out sessionId);

        if(isGuid){
            cart = cartRepository.FirstOrDefault(x=> x.SessionId == sessionIdAsString);
        }

        if (cart == null)
        {
            cart = GenerateNew();
            cartRepository.Add(cart);
        }

        return cart;
    }

    private Models.Cart GenerateNew() => new Models.Cart(Guid.NewGuid().ToString(), new List<CartItem>());
}
