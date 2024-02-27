using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;

public enum OrderStatus
{
    New,
    Preparing,
    SendingToCustomer,
    Delivered
}

[Table("orders")]
public class OrderEntity
{
    public OrderEntity()
    {
        Itens = new List<OrderItemEntity>();
    }
    public Guid Id { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public required CustomerEntity Customer { get; set; }
    public required List<OrderItemEntity> Itens { get; set; }
    public OrderStatus Status { get; set; }
}
