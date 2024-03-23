docker rm -f grafana
docker run -d \
  -p 3000:3000 \
  -e GF_PATHS_CONFIG=/grafana/grafana.ini \
  --name grafana \
  -v grafana:/grafana \
  -v grafana_data:/var/lib/grafana \
  grafana