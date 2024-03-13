
using ClothesVirtualStore.Api.Products.Data.Context;
using ClothesVirtualStore.Api.Products.Data.Models;
using ClothesVirtualStore.Api.Products.Data.ViewModels;

namespace ClothesVirtualStore.Api.Products.Endpoints;
public class ProductsEndPoints
{
    public void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/", (IProductsRepository repository) => repository.GetAll());
        app.MapGet("/products/{id}", IResult (IProductsRepository repository, Guid id) => {
            var product =  repository.GetById(id);
            return product is Product ? TypedResults.Ok(product) : TypedResults.NotFound();
        });
        app.MapPost("/products/", (ProductViewModel body, IProductsRepository repository) => repository.Insert(body.ToDbModel()));
    }
}