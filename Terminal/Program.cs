using System.Globalization;
using library.worldcomputer.info;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using terminal.worldcomputer.info;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGrid>((services) =>
{
    var grid = new GridShell();

    return grid;
});

builder.Services.AddSingleton<IStatus, Status>();

builder.Services.AddScoped<ITotp, TotpWrapper>();
builder.Services.AddScoped<IWordResolver, WordNet>();
builder.Services.AddScoped<IMove, Mover>();
builder.Services.AddScoped<ICmdParser, CmdParser>();
builder.Services.AddScoped<IUxLogin<IUnit>, SocketUxLogin>();
builder.Services.AddScoped<IUxNewPlayer<IUnit>, SocketUxNewPlayer>();
builder.Services.AddScoped<IUxEnrollTotp<IUnit, IUnit>, SocketUxEnrollTotp>();
builder.Services.AddScoped<IUxKnownPlayer<IUnit, string>, SocketUxKnownPlayer>();
builder.Services.AddScoped<IUxGameLoop<IUnit, IUnit>, SocketUxGameLoop>();
builder.Services.AddScoped<IUxChallengeTotp<bool, IUnit>, SocketUxTotpChallenge>();
builder.Services.AddScoped<IDal<Body>, FlatFileDb<Body>>();
builder.Services.AddScoped<IDal<IUnit>, FlatFileDb<IUnit>>();
builder.Services.AddSingleton<IImageHandler, AnsiImageHandler>();

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
