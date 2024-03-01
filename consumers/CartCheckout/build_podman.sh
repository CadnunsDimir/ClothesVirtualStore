
rm -r bin/ClothesVirtualStore.CommonsLib

cp -a ../../commons/ClothesVirtualStore.CommonsLib bin/ClothesVirtualStore.CommonsLib/

podman rmi $(podman images 'microservice_cart_consumer' -a -q)
podman build . -t 'microservice_cart_consumer'

rm -r bin/ClothesVirtualStore.CommonsLib