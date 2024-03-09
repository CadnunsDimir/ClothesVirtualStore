using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;

[Table("orders_itens")]
public class OrderItemEntity
{
    public Guid Id { get; set; }    
    public decimal TotalValue { get; set; }
    public required string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductAmount { get; set; }
    public OrderEntity? Order { get; set; }
}