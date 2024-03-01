# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG env

COPY ./bin/ClothesVirtualStore.CommonsLib/ /commons/ClothesVirtualStore.CommonsLib

WORKDIR /source
COPY . .

RUN dotnet restore *.csproj

RUN dotnet publish *.csproj -c ${env} -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "ClothesVirtualStore.MicroServices.Consumers.CartCheckout.dll"]