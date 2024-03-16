#!/bin/bash 

podman compose -f docker-compose.yml stop
podman compose -f docker-compose.yml start