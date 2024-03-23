using ClothesVirtualStore.Api.Token.Data;
using ClothesVirtualStore.Commons.Auth;
using ClothesVirtualStore.Commons.Auth.Data;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IUserDbContext, UserDbContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<AuthUseCases>();
builder.Services.AddClothesVirtualStoreAuthentication();

var app = builder.Build();

app.MapPost("/token", IResult ([FromBody]LoginViewModel login, AuthUseCases useCases) => {
    var user = useCases.GetByCredentials(login.Email, login.Password);
    if(user == null){
        return TypedResults.NotFound(new {message = "Usuário ou senha inválidos"});
    }
        
    var token = TokenService.GenerateToken(user);
    return  TypedResults.Ok (new {
        user = new {
            user.UserName,
            user.Email,
            user.Role
        },
        token
    });
});

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    if(context.Database.EnsureCreated()){
        var admin = context.CreateAdminUser();
        Console.WriteLine("Admin was created");
        Console.WriteLine(admin);
    }
}

app.UseClothesVirtualStoreAuthentication();

app.Run();

record LoginViewModel(string Email, string Password);
