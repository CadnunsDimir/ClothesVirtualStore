
using ClothesVirtualStore.Api.Products.Endpoints;
using ClothesVirtualStore.Api.Products.Models;
using ClothesVirtualStore.Api.Products.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ProductsEndPoints>();
builder.Services.AddTransient<IProductsRepository, ProductsRepository>();

var app = builder.Build();
var endpoints = app.Services.GetService<ProductsEndPoints>();
endpoints?.Map(app);

app.Run();