using System.Text.Json;
using ClothesVirtualStore.Api.Products.Data.Context;
using ClothesVirtualStore.Api.Products.Data.Models;

namespace ClothesVirtualStore.Api.Products.Data.Repositories;

public class ProductsRepository : IProductsRepository
{
    // private readonly List<Product> products;
    private readonly IProductsDbContext dbContext;

    public ProductsRepository(IProductsDbContext dbContext)
    {
        this.dbContext = dbContext;
        
        // var options = new JsonSerializerOptions (JsonSerializerDefaults.Web)
        // {
        //     WriteIndented = true
        // };

        // var json = "[{\"id\":\"a5e7c482-49cc-4315-b360-3c6c02feb461\",\"name\":\"Camiseta Masculina M\",\"price\":20.50},{\"id\":\"b2b2c417-ebbb-45a2-995b-c5d41b9189f1\",\"name\":\"Camiseta Feminina P\",\"price\":25.50},{\"id\":\"c5856888-24dd-420d-b949-6d229809c867\",\"name\":\"Calça Jeans Masculina G\",\"price\":20.50},{\"id\":\"10b8c1fb-18e2-45af-a4b9-399f09016595\",\"name\":\"Calça Jeans Feminina M\",\"price\":25.50}]";

        // this.products = JsonSerializer.Deserialize<List<Product>>(json, options) ?? new List<Product>();
    }

    public List<Product> GetAll() => dbContext.Products.ToList();

    public Product? GetById(Guid id)=> dbContext.Products.FirstOrDefault(x=> x.Id == id);

    public Product? Insert(Product product)
    {
        dbContext.Products.Add(product);
        return (dbContext.SaveChanges() > 0) ? product : null;
    }
}