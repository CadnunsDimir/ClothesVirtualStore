# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG APP_ENVIRONMENT

COPY ./bin/ClothesVirtualStore.CommonsLib/ /commons/ClothesVirtualStore.CommonsLib

WORKDIR /source
COPY . .

RUN dotnet restore *.csproj

RUN dotnet publish *.csproj -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ClothesVirtualStore.Api.Cart.dll", "--environment=Container"]
EXPOSE 8080