

using ClothesVirtualStore.Api.Products.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothesVirtualStore.Api.Products.Data.Context;
public class ProductsDbContext : DbContext, IProductsDbContext
{
    private readonly IConfiguration configuration;

    public DbSet<Product> Products { get; private set; }

    public ProductsDbContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Console.WriteLine($"MySql.Connection: {_configuration.GetConnectionString(AppConstants.DbName)}");
        
        optionsBuilder.UseMySQL(configuration.GetConnectionString("MySql") ?? "null");
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    public void InitDatabase()
    {
        Database.EnsureCreated();
    }
}