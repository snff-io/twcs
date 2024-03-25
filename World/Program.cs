using library.worldcomputer.info;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Processor>();
builder.Services.AddSingleton<IWordResolver, WordNet>();
builder.Services.AddSingleton<IStatus, Status>();
builder.Services.AddSingleton<IMove, Mover>();
builder.Services.AddSingleton<ICmdParser, CmdParser>();
builder.Services.AddSingleton<InputProcessorWebSocket>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseWebSockets();


app.Use(async (context, next) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var handler = app.Services.GetRequiredService<InputProcessorWebSocket>();
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await handler.HandleWebSocket(context, webSocket);
    }
    else
    {
        await next();
    }
});

app.Run("http://100.115.92.204:5260");
