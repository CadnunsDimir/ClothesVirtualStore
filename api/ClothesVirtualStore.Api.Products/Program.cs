
using ClothesVirtualStore.Api.Products.Data.Context;
using ClothesVirtualStore.Api.Products.Data.Repositories;
using ClothesVirtualStore.Api.Products.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ProductsEndPoints>();
builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
builder.Services.AddDbContext<IProductsDbContext, ProductsDbContext>();

var app = builder.Build();
var endpoints = app.Services.GetService<ProductsEndPoints>();
endpoints?.Map(app);

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<IProductsDbContext>();
    context.InitDatabase();
}
app.Run();