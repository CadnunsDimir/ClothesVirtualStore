using System.Text;
using ClothesVirtualStore.Commons.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static class AuthExtensions
{
    public static void AddClothesVirtualStoreAuthentication(this IServiceCollection services)
    {
        var key = Encoding.ASCII.GetBytes(Settings.TokenSecret);
        
        services.AddAuthentication(x=>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x=> {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddAuthorization();
    }

    public static void UseClothesVirtualStoreAuthentication(this WebApplication app){
        app.UseAuthentication();
        app.UseAuthorization();
    }
}