#!/bin/bash 

COMMON_LIB=ClothesVirtualStore.CommonsLib

AUTH_LIB=ClothesVirtualStore.Commons.Auth

rm -r consumers/CartCheckout/bin/$COMMON_LIB
cp -a commons/$COMMON_LIB consumers/CartCheckout/bin/$COMMON_LIB/
rm -r api/ClothesVirtualStore.Api.Cart/bin/$COMMON_LIB
cp -a commons/$COMMON_LIB api/ClothesVirtualStore.Api.Cart/bin/$COMMON_LIB/

API_TOKEN_BIN=api/ClothesVirtualStore.Api.Token/bin
rm -r $API_TOKEN_BIN/$AUTH_LIB
cp -a commons/$AUTH_LIB $API_TOKEN_BIN/$AUTH_LIB/

API_PRODUCTS_BIN=api/ClothesVirtualStore.Api.Products/bin
rm -r $API_PRODUCTS_BIN/$AUTH_LIB
cp -a commons/$AUTH_LIB $API_PRODUCTS_BIN/$AUTH_LIB/

podman compose -f docker-compose.yml up --build --detach

# remove orphans/dangling images
podman image prune --force