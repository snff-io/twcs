using Microsoft.AspNetCore.Mvc;
using Prometheus;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using library.worldcomputer.info;
using System.Text.Json.Serialization;


namespace World.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class GridController : ControllerBase
    {
        static object the_grid_lock = new object();
        static Grid? the_grid = null;
        private readonly Processor _processor;
        private readonly ILogger<GridController> _logger;

        public GridController(ILogger<GridController> logger, Processor processor)
        {
            _processor = processor;
            _logger = logger;
            CreateMetrics();
        }

        [HttpGet("/grid/pairgroup/{layer}/{x}/{y}")]
        public IActionResult PairGroup(int layer, int x, int y)
        {
            if (the_grid == null)
            {
                return StatusCode(400, "Bad Reqeust. Grid not initialized.");
            }

            var pg = new PairGroup(the_grid, layer, x, y);

            var json = JsonSerializer.Serialize(pg, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            HttpContext.Response.ContentType = "application/json";
            return Content(json);
        }

        [HttpGet("/grid/init")]
        public async Task<IActionResult> Init()
        {
            if (!Monitor.TryEnter(the_grid_lock, TimeSpan.FromSeconds(1)))
            {
                return await Task.FromResult(StatusCode(503, "Server busy. Try again later."));
            }
            try
            {
                EnsureGridInitialized();
                await _grid_registry.CollectAndExportAsTextAsync(Response.Body);
                return new EmptyResult();
            }
            finally
            {
                Monitor.Exit(the_grid_lock);
            }
        }

        [HttpGet("/grid/process")]
        public async Task<IActionResult> Process()
        {
            if (!Monitor.TryEnter(the_grid_lock, TimeSpan.FromSeconds(1)))
            {
                return await Task.FromResult(StatusCode(503, "Server busy. Try again later."));
            }
            try
            {
                EnsureGridInitialized();
                var sw_turn = Stopwatch.StartNew();
                _processor.SimulateSingleTurn(the_grid);
                sw_turn.Stop();

                m_turn_processing_time.WithLabels("elapsed").Observe(sw_turn.ElapsedMilliseconds);
                await _grid_registry.CollectAndExportAsTextAsync(Response.Body);
                return new EmptyResult();
            }
            finally
            {
                Monitor.Exit(the_grid_lock);
            }
        }

        [HttpGet("/grid/metrics")]
        public async Task<IActionResult> StatMetrics(int startLayer = 0, int endLayer = 2)
        {
            if (the_grid == null)
            {
                return StatusCode(400, "Bad Reqeust. Grid not initialized.");
            }

            var allPairs = the_grid.Layers
                .Where((layer, index) => index >= startLayer && index <= endLayer) // Filter layers within the specified range
                .SelectMany(layer => layer.SelectMany(row => row));

            Parallel.ForEach(allPairs, pair =>
            {
                m_grid_pairs_stat.WithLabels(
                    pair.Layer.ToString(),
                    pair.X.ToString(),
                    pair.Y.ToString(),
                    pair.MaxMagnitude.ToString(),
                    pair.Magnitude.ToString(),
                    pair.Pressure.ToString(),
                    pair.Stability.ToString(),
                    pair.TopType.ToString(),
                    pair.BottomType.ToString());
            });

            await _grid_registry.CollectAndExportAsTextAsync(Response.Body);
            return new EmptyResult();
        }

        private void EnsureGridInitialized()
        {
            if (the_grid == null)
            {
                the_grid = new Grid();

                var sw_init = Stopwatch.StartNew();
                the_grid.InitializeGrid();
                sw_init.Stop();

                m_grid_init_time.WithLabels("elapsed").Observe(sw_init.ElapsedMilliseconds);
            }
        }
    }
}
