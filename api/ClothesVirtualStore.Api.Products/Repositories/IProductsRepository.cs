using ClothesVirtualStore.Api.Products.Models;

namespace ClothesVirtualStore.Api.Products.Repositories;
public interface IProductsRepository
{
    List<Product> GetAll();
    Product? GetById(Guid id);
}