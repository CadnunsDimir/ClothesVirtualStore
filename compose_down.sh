#!/bin/bash 

podman compose -f docker-compose-microservices.yml down
podman compose -f docker-compose.yml down
