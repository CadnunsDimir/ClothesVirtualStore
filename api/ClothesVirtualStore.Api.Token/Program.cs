using ClothesVirtualStore.Api.Token;
using ClothesVirtualStore.Commons.Auth;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddClothesVirtualStoreAuthentication();

var app = builder.Build();

app.MapPost("/token", IResult ([FromBody]LoginViewModel login, UserRepository repository) => {
    var user = repository.Get(login.Email, login.Password);
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

app.UseClothesVirtualStoreAuthentication();

app.Run();

record LoginViewModel(string Email, string Password);
