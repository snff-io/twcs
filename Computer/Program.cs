using Computer;
using library.worldcomputer.info;

var builder = Host.CreateApplicationBuilder(args);

IGrid grid = new Grid();
grid.InitializeGrid();

builder.Services.AddSingleton<IGrid>(grid);
builder.Services.AddSingleton<IInput, ConsoleInput>();
builder.Services.AddSingleton<IInformer, ConsoleInformer>();
builder.Services.AddSingleton<IStatus, Status>();
builder.Services.AddSingleton<IProcessor, Processor>();
builder.Services.AddSingleton<IMove, Mover>();
builder.Services.AddSingleton<Loop>();
builder.Services.AddSingleton<IPlayer, Player>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();



var serviceProvider = builder.Services.BuildServiceProvider();

var player = serviceProvider.GetRequiredService<IPlayer>();
player.Name = "joshua";
player.Location = new Location();

var loop = serviceProvider.GetRequiredService<Loop>();

//loop.Run(player);
var task = Task.Run(() => loop.Run(player));


host.Run(); 


