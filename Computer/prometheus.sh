docker rm -f prometheus
docker run \
  -d \
  -p 9090:9090 \
  --name prometheus \
  -v prometheus:/prometheus \
  twc_prometheus:latest