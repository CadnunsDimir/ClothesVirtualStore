
using ClothesVirtualStore.CommonsLib.Models;

namespace ClothesVirtualStore.Api.Cart.Models;

public record Cart(string SessionId, List<CartItem> Itens);
