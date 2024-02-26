namespace ClothesVirtualStore.CommonsLib.Models;

public record Order(Guid orderId, string cpf, List<CartItem> itens);