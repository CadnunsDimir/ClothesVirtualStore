using ClothesVirtualStore.Api.Cart;
using ClothesVirtualStore.Api.Cart.Models;
using ClothesVirtualStore.CommonsLib.Models;

namespace ClothesVirtualStore.Api.Cart.Services;

public interface ICartService
{
    Task<Models.Cart?> GetCart(string sessionId);
    Order Checkout(Models.Cart cart, string cpf);
    void UpdateCart(Models.Cart cart);
    Task<Models.Cart?> CreatOrGetCart(string? sessionId);
}
