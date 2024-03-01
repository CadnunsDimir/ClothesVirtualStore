#!/bin/bash 

MICROSERVICE_NAME=cart_consumer_microservice
COMMON_LIB=ClothesVirtualStore.CommonsLib
NET_NAME=virtualstore_network

rm -r bin/$COMMON_LIB

cp -a ../../commons/$COMMON_LIB bin/$COMMON_LIB/

podman rm $MICROSERVICE_NAME
podman rmi $(podman images $MICROSERVICE_NAME -a -q)
# podman network rm $NET_NAME

podman build . -t $MICROSERVICE_NAME --build-arg env=LocalPodman

rm -r bin/$COMMON_LIB

# podman network create -d macvlan -o parent=eth0 --subnet 255.255.255.0/24  $NET_NAME
# podman run --network=$NET_NAME --name $MICROSERVICE_NAME $MICROSERVICE_NAME

podman run -d --name $MICROSERVICE_NAME $MICROSERVICE_NAME