#!/bin/bash 

podman compose -f docker-compose.yml stop
podman compose -f docker-compose-microservices.yml stop

podman compose -f docker-compose.yml start
podman compose -f docker-compose-microservices.yml start

echo "[api.products] Test rabbitmq admin"
podman exec -ti virtualstoreapiproducts curl http://localhost:15672/#/