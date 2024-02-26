using ClothesVirtualStore.Api.Cart;
using ClothesVirtualStore.Api.Cart.Models;

namespace ClothesVirtualStore.Api.Cart.Services;

public interface ICartService
{
    Models.Cart GetCart(string sessionId);
    Order Checkout(Models.Cart cart, string cpf);
    object Checkout(Models.Cart cart, object cpf);
}
