#!/bin/bash 
MICROSERVICE_NAME=virtualstore_api_products

podman rm $MICROSERVICE_NAME
podman build . -t $MICROSERVICE_NAME
podman run --detach -p 8080:8080 -d --name $MICROSERVICE_NAME $MICROSERVICE_NAME