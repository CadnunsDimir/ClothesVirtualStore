#!/bin/bash 

podman compose -f docker-compose.yml stop
podman compose -f docker-compose-microservices.yml stop

podman compose -f docker-compose.yml start
podman compose -f docker-compose-microservices.yml start

echo "[mysql] Test rabbitmq"
podman exec -ti mysql ping rabbitmq

echo "[consumercartcheckout] Test mysql"
podman exec -ti consumercartcheckout ping mysql

echo "[consumercartcheckout] Test rabbitmq"
podman exec -ti consumercartcheckout ping rabbitmq

echo "Check container's network connection"
podman inspect consumercartcheckout -f "{{json .NetworkSettings.Networks }}"