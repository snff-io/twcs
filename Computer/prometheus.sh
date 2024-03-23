docker rm -f prometheus
docker run \
  -it \
  -p 9090:9090 \
  --name prometheus \
  -v prometheus:/prometheus \
  prometheus:latest