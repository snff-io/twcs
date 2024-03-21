FROM prom/prometheus

# Create a volume for Prometheus configuration and data
VOLUME ["/prometheus"]

# Copy the Prometheus configuration file into the volume directory
COPY prometheus.yml /prometheus/prometheus.yml


# Expose the port Prometheus listens on
EXPOSE 9090

# Define the command to run the Prometheus server
CMD ["--config.file=/prometheus/prometheus.yml", "--storage.tsdb.path=/prometheus/data"]
