namespace ClothesVirtualStore.Api.Cart.Models;

public record Order(Guid orderId, string cpf, List<CartItem> itens);