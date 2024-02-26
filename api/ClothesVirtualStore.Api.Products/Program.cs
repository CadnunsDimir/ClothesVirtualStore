

using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var options = new JsonSerializerOptions (JsonSerializerDefaults.Web)
            {
                WriteIndented = true
            };

var app = builder.Build();
var json = "[{\"id\":\"a5e7c482-49cc-4315-b360-3c6c02feb461\",\"name\":\"Camiseta Masculina M\",\"price\":20.50},{\"id\":\"b2b2c417-ebbb-45a2-995b-c5d41b9189f1\",\"name\":\"Camiseta Feminina P\",\"price\":25.50},{\"id\":\"c5856888-24dd-420d-b949-6d229809c867\",\"name\":\"Calça Jeans Masculina G\",\"price\":20.50},{\"id\":\"10b8c1fb-18e2-45af-a4b9-399f09016595\",\"name\":\"Calça Jeans Feminina M\",\"price\":25.50}]";

// var products = new List<Product>{
//     new Product{ Id = Guid.NewGuid(), Name = "Camiseta Masculina M", Price = 20.50M },
//     new Product{ Id = Guid.NewGuid(), Name = "Camiseta Feminina P", Price = 25.50M },
//     new Product{ Id = Guid.NewGuid(), Name = "Calça Jeans Masculina G", Price = 20.50M },
//     new Product{ Id = Guid.NewGuid(), Name = "Calça Jeans Feminina M", Price = 25.50M }
// };

var products = JsonSerializer.Deserialize<List<Product>>(json, options);

app.MapGet("/products", () => products);

app.Run();

internal class Product
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
}