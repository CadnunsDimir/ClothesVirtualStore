
using ClothesVirtualStore.Api.Products.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesVirtualStore.Api.Products.Data.Context;
public interface IProductsDbContext
{
    DbSet<Product> Products { get;  }

    void InitDatabase();
    int SaveChanges();
}