
using ClothesVirtualStore.Api.Products.Models;
using ClothesVirtualStore.Api.Products.Repositories;
using Microsoft.AspNetCore.Builder;
using System.Text.Json;

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
    }
}