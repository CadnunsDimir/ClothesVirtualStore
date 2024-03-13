using ClothesVirtualStore.Api.Cart;
using ClothesVirtualStore.Api.Cart.Models;
using ClothesVirtualStore.Api.Cart.Services;
using ClothesVirtualStore.CommonsLib.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataProtection();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IMQProcessingService, RabbitMQService>();
builder.Services.AddScoped<ICachingService<Cart>, CachingService>();
builder.Services.AddStackExchangeRedisCache(options =>
 {
     options.Configuration = builder.Configuration.GetConnectionString("redis");
     options.InstanceName = "Cart";
 });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/cart", ([FromHeader] string? cartId, ICartService service, HttpContext httpContext) => {
    Console.WriteLine(cartId);
    return service.CreatOrGetCart(cartId);
});

app.MapPost("/cart/item", async Task<Results<Ok<Cart>, NotFound>> (CartItem body, [FromHeader()] string cartId, ICartService service, HttpContext httpContext) => {
    var cart = await service.GetCart(cartId);
    if (cart == null)
    {
        return TypedResults.NotFound();
    }
    cart.Itens.Add(body);
    service.UpdateCart(cart);
    return TypedResults.Ok(cart);
});

app.MapPost("/cart/checkout", async Task<Results<Ok<Order>, NotFound>> (CheckoutCart body, [FromHeader()] string cartId, ICartService service, HttpContext httpContext) => {
    if (string.IsNullOrEmpty(cartId))
    {
        throw new UnauthorizedAccessException("'cardId' header not defined");
    }

    var cart = await service.GetCart(cartId);
    if (cart == null)
    {
        return TypedResults.NotFound();
    }
    var order = service.Checkout(cart, body.Cpf);
    return TypedResults.Ok(order);
});

app.Run();
