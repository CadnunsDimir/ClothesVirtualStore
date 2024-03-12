# ClothesVirtualStore

O objetivo desse projeto é demonstrar como aplicar o conceito de microserviços utilizando containers podman (trocando o comando podman por docker, é possivel subir a mesma infra-estrutura utilizando o docker).

## Como configurar o ambiente local (Podman)

O comando abaixo criará serviços em forma de containers:

```bash
./compose_up.sh
```
Uma vez criados, podemos usar o comando abaixo para reiniciá-los:

```bash
./compose_start.sh
```
Para remover todos serviços criados, use o comando:

```bash
./compose_start.sh
```
Obs.: o comando acima não remove as imagens criadas, somente remove os container.
