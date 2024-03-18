# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build_api_products

COPY ./bin/ClothesVirtualStore.Commons.Auth/ /commons/ClothesVirtualStore.Commons.Auth

WORKDIR /source

COPY . .
RUN dotnet restore
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build_api_products /app ./

ENTRYPOINT ["dotnet", "ClothesVirtualStore.Api.Products.dll"]

# IF NECESSARY, EXPOSE PORT 80
EXPOSE 8080