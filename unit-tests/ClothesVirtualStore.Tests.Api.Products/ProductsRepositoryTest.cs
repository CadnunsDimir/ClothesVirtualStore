using ClothesVirtualStore.Api.Products.Repositories;

namespace ClothesVirtualStore.Tests.Api.Products.Repositories;
public class ProductsRepositoryTest
{
    ProductsRepository repository = new ProductsRepository();
    [Fact]
    public void GetAll() {
        var productsAmount = repository.GetAll().Count;
        Assert.NotEqual(0, productsAmount);
    }
}