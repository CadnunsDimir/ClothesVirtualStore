# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build_consumer_cart

ARG APP_ENVIRONMENT

COPY ./bin/ClothesVirtualStore.CommonsLib/ /commons/ClothesVirtualStore.CommonsLib

WORKDIR /source
COPY . .

RUN dotnet restore *.csproj

RUN echo Nome do ambiente ${APP_ENVIRONMENT}

RUN dotnet publish *.csproj -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build_consumer_cart /app ./
ENTRYPOINT dotnet ClothesVirtualStore.MicroServices.Consumers.CartCheckout.dll --environment=LocalPodman