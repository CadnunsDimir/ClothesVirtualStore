using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Context;

public class VirtualStoreDbContext : DbContext, IVirtualStoreDbContext
{
    public DbSet<CustomerEntity> Customer { get; private set; }
    public DbSet<OrderEntity> Order { get; private set; }
    public DbSet<OrderItemEntity> OrderItem { get; private set; }

    public void PrepareDataBase()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=localhost;DataBase=VirtualStore;Uid=root;Pwd=123456");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CustomerEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Cpf).IsRequired();
            entity.Property(e => e.FullName).IsRequired();
        });

        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PurchaseDateTime).IsRequired();
            entity.HasOne(e => e.Customer);
            entity.HasMany(e => e.Itens)
                .WithOne(p => p.Order);
        });

        modelBuilder.Entity<OrderItemEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalValue).IsRequired();
            entity.Property(e => e.ProductName).IsRequired();
            entity.Property(e => e.ProductPrice).IsRequired();
            entity.Property(e => e.ProductAmount).IsRequired();
            entity.HasOne(e => e.Order);
        });
    }
}
