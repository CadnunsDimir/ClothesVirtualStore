using ClothesVirtualStore.Api.Products.Data.Models;

namespace ClothesVirtualStore.Api.Products.Data.Context;
public interface IProductsRepository
{
    List<Product> GetAll();
    Product? GetById(Guid id);
    Product? Insert(Product product);
}