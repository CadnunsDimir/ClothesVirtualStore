#!/bin/bash 

COMMON_LIB=ClothesVirtualStore.CommonsLib

rm -r consumers/CartCheckout/bin/$COMMON_LIB
cp -a commons/$COMMON_LIB consumers/CartCheckout/bin/$COMMON_LIB/
rm -r  api/ClothesVirtualStore.Api.Cart/bin/$COMMON_LIB
cp -a commons/$COMMON_LIB api/ClothesVirtualStore.Api.Cart/bin/$COMMON_LIB/

podman compose -f docker-compose.yml up --detach
podman compose -f docker-compose-microservices.yml up --build --detach

# remove orphans/dangling images
podman image prune --force