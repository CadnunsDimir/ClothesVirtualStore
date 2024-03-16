#!/bin/bash 

podman compose -f docker-compose.yml down

# podman image rm docker.io/library/consumercartcheckout
# podman image rm docker.io/library/virtualstoreapicart
