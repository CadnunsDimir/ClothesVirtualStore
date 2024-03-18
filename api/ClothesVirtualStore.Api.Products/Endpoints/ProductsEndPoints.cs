
using ClothesVirtualStore.Api.Products.Data.Context;
using ClothesVirtualStore.Api.Products.Data.Models;
using ClothesVirtualStore.Api.Products.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        app.MapPost("/products/", 
            [Authorize(Roles = "Admin")] 
            (ProductViewModel body, IProductsRepository repository, HttpContext context) => repository.Insert(body.ToDbModel()));
    }
}