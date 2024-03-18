
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
        app.MapPost("/products/", [Authorize(Roles = "Admin")] (ProductViewModel body, IProductsRepository repository, HttpContext context) => 
        {
            var user = context.User;
            var userIdentity = (ClaimsIdentity?)user.Identity;

            if(userIdentity != null) 
            {
                var claims = userIdentity.Claims;
                var roleClaimType = userIdentity.RoleClaimType;
                var roles = claims.Where(c => c.Type == ClaimTypes.Role || c.Type == "Role").ToList();
                roles.ForEach(x=> Console.WriteLine($"RoleType={x.Type}, Value={x.Value}, ValueType={x.ValueType}"));               
            } else
            {
                Console.WriteLine("'userIdentity' is null");
            }          

            return repository.Insert(body.ToDbModel());
        });
    }
}