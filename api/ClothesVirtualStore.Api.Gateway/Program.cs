using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"api-gateway.{builder.Environment.EnvironmentName}.json");
builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();