using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Context;

public interface IVirtualStoreDbContext
{
    public DbSet<CustomerEntity> Customer { get; }
    public DbSet<OrderEntity> Order { get; }
    public DbSet<OrderItemEntity> OrderItem { get; }

    void PrepareDataBase();
    int SaveChanges();
}
