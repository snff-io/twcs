using System.Globalization;
using library.worldcomputer.info;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using terminal.worldcomputer.info;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGrid>((services) =>
{
    var grid = new Grid(){
        LayerSize = 190
    };

    grid.InitializeGrid();

    return grid;
});

builder.Services.AddSingleton<IStatus, Status>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<ITotp, TotpWrapper>();
builder.Services.AddSingleton<IWordResolver, WordNet>();

builder.Services.AddScoped<IUxLogin<IUnit>, SocketUxLogin>();
builder.Services.AddScoped<IUxNewPlayer<IUnit>, SocketUxNewPlayer>();
builder.Services.AddScoped<IUxEnrollTotp<IUnit, IUnit>, SocketUxEnrollTotp>();
builder.Services.AddScoped<IUxKnownPlayer<IUnit, string>, SocketUxKnownPlayer>();
builder.Services.AddScoped<IUxGameLoop<IUnit, IUnit>, SocketUxGameLoop>();
builder.Services.AddScoped<IUxChallengeTotp<bool, IUnit>, SocketUxTotpChallenge>();
builder.Services.AddScoped<IDal<Body>, FlatFileDb<Body>>();

builder.Services.AddScoped<IDal<Spirit>, FlatFileDb<Spirit>>();
builder.Services.AddScoped<IDal<IUnit>, FlatFileDb<IUnit>>();
builder.Services.AddSingleton<IImageHandler, AnsiImageHandler>();


builder.Services.AddScoped<IIntentAction, MarketIntentAction>();
builder.Services.AddScoped<IIntentAction, TravelIntentAction>();
builder.Services.AddScoped<IIntentAction, LocationIntentAction>();

builder.Services.AddSingleton<IIntent, MarketIntent>();
builder.Services.AddSingleton<IIntent, TravelIntent>();
builder.Services.AddSingleton<IIntent, LocationIntent>();



var app = builder.Build();


app.UseStaticFiles();

app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using (var scope = app.Services.CreateScope())
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();

            var wordResolver = scope.ServiceProvider.GetRequiredService<IWordResolver>();
            var socket = new Socket(webSocket, wordResolver);

            var handler = scope.ServiceProvider.GetRequiredService<IUxLogin<IUnit>>();

            await handler.HandleUx(socket);
        }
    }
    else
    {
        await next();
    }
});

app.Run("http://100.115.92.204:5260");
//app.Run();
