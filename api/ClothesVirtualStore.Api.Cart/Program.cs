using ClothesVirtualStore.Api.Cart;
using ClothesVirtualStore.Api.Cart.Models;
using ClothesVirtualStore.Api.Cart.Services;
using ClothesVirtualStore.CommonsLib.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Primitives;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
builder.Services.AddDataProtection();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IMQProcessingService, RabbitMQService>();
builder.Services.AddScoped<ICachingService, CachingService>();
builder.Services.AddStackExchangeRedisCache(options =>
 {
     options.Configuration = builder.Configuration.GetConnectionString("redis");
     options.InstanceName = "Cart";
 });

var app = builder.Build();

app.MapGet("/cart", (ICartService service, HttpContext httpContext) => {
    var cardId = getCartIdFromHeader(httpContext); 
    return service.CreatOrGetCart(cardId);
});

app.MapPost("/cart/{cartId}/item", async Task<Results<Ok<Cart>, NotFound>> (string cartId, CartItem body, ICartService service, HttpContext httpContext) => {
    var cart = await service.GetCart(cartId);
    if (cart == null)
    {
        return TypedResults.NotFound();
    }
    cart.Itens.Add(body);
    service.UpdateCart(cart);
    return TypedResults.Ok(cart);
});

app.MapPost("/cart/checkout", async Task<Results<Ok<Order>, NotFound>> (CheckoutCart body, ICartService service, HttpContext httpContext) => {
    var cardId = getCartIdFromHeader(httpContext);

    if (string.IsNullOrEmpty(cardId))
    {
        throw new UnauthorizedAccessException("'cardId' header not defined");
    }

    var cart = await service.GetCart(cardId);
    if (cart == null)
    {
        return TypedResults.NotFound();
    }
    var order = service.Checkout(cart, body.Cpf);
    return TypedResults.Ok(order);
});

string? getCartIdFromHeader(HttpContext httpContext)
{
    StringValues cartIdHeader;
    if(httpContext.Request.Headers.TryGetValue("cartId", out cartIdHeader)){
        return cartIdHeader.First();
    }
    return null;
}

app.UseSession();
app.Run();
