using ClothesVirtualStore.Api.Cart;
using ClothesVirtualStore.Api.Cart.Models;

namespace ClothesVirtualStore.Api.Cart.Services;

public interface IMQProcessingService
{
    void EnqueueOrder(Order order);
}
