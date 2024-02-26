using ClothesVirtualStore.Api.Cart;
using ClothesVirtualStore.Api.Cart.Models;
using ClothesVirtualStore.Api.Cart.Services;

var sessionIdkey = "sessionId";

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
var app = builder.Build();

app.MapGet("/cart", (ICartService service, HttpContext httpContext) => {
    var sessionId = getSessionId(httpContext.Session);
    return service.GetCart(sessionId);
});

app.MapPost("/cart/checkout", (CheckoutCart body, ICartService service, HttpContext httpContext) => {
    var sessionId = getSessionId(httpContext.Session);
    var cart = service.GetCart(sessionId);
    var order = service.Checkout(cart, body.Cpf);
    clearSessionId(httpContext.Session);
    return order;
});

void clearSessionId(ISession session)
{
    session.Clear();
}

string getSessionId(ISession session)
{
    var sessionId = session.GetString(sessionIdkey);
    if(sessionId == null) {
        sessionId = Guid.NewGuid().ToString();
        session.SetString(sessionIdkey, sessionId);
    }
    return sessionId;
}

app.UseSession();
app.Run();
