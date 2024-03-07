#!/bin/bash 

COMMON_LIB=ClothesVirtualStore.CommonsLib

rm -r consumers/CartCheckout/bin/$COMMON_LIB
cp -a commons/$COMMON_LIB consumers/CartCheckout/bin/$COMMON_LIB/
rm -r  api/ClothesVirtualStore.Api.Cart/bin/$COMMON_LIB
cp -a commons/$COMMON_LIB api/ClothesVirtualStore.Api.Cart/bin/$COMMON_LIB/

# podman network create clothesvirtualstore_network

podman compose -f docker-compose.yml up --detach
podman compose -f docker-compose-microservices.yml up --detach

echo Aguardando ...
sleep 10

echo "[api.products] Test rabbitmq admin"
podman exec -ti virtualstoreapiproducts curl http://rabbitmq:15672