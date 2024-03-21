docker rm -f grafana
docker run -it \
  -p 3000:3000 \
  -e GF_PATHS_CONFIG=/grafana/grafana.ini \
  --name grafana \
  -v grafana:/grafana \
  twc_grafana