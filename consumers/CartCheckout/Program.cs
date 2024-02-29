
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Context;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Repositories;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.MQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<CartCheckoutWorker>();
builder.Services.AddSingleton<RabbitMQService>();
builder.Services.AddDbContext<IVirtualStoreDbContext, VirtualStoreDbContext>();
builder.Services.AddSingleton<IStoreProductsRepository, StoreProductsRepository>();
builder.Services.AddSingleton<CartUseCases>();

var host = builder.Build();
host.Run();