using library.worldcomputer.info;
using Microsoft.AspNetCore.Mvc;
using Prometheus;


namespace World.Controllers
{
    using System.Diagnostics;
    using System.Text;
    using System.Threading;
    using Microsoft.VisualBasic;
    using MongoDB.Bson;

    public partial class GridController : ControllerBase
    {
        private CollectorRegistry _grid_registry;
        private Histogram m_grid_pairs_stat;
        private Histogram m_turn_processing_time;
        private Histogram m_grid_init_time;

        public void CreateMetrics()
        {
            var hconfig = new HistogramConfiguration
            {
                LabelNames = new[] { "label1", "label2" }, // Define label names for the histogram
                Buckets = new[] { 1.0 } // Define custom bucket boundaries
            };

            _grid_registry = Metrics.NewCustomRegistry();

            this.m_grid_pairs_stat = Metrics.WithCustomRegistry(_grid_registry)

            .CreateHistogram("grid_pairs_stat", "Topology status",
            new[] { "layer", "x", "y", "maxmag", "mag", "pres", "stab", "topt", "bott" }, hconfig);

            this.m_turn_processing_time = Metrics.WithCustomRegistry(_grid_registry)
            .CreateHistogram("turn_processing_time", "Time taken to process grid topology", new[] { "elapsed" }, hconfig);

            this.m_grid_init_time = Metrics.WithCustomRegistry(_grid_registry)
            .CreateHistogram("grid_init_time", "Time taken to initialize grid topology", new[] { "elapsed" }, hconfig);
        }
    }
}