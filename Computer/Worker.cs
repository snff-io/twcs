using library.worldcomputer.info;
namespace Computer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IGrid _grid;
        private IProcessor _processor;

        public IGrid Grid => _grid;

        public Worker(ILogger<Worker> logger, IGrid grid, IProcessor processor)
        {
            _logger = logger;
            _grid = grid;
            _processor = processor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _grid.InitializeGrid();

            while (!stoppingToken.IsCancellationRequested)
            {
                _processor.Process(_grid);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
