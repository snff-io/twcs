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
builder.Services.AddScoped<IUxLogin<Player>, SocketUxLogin>();
builder.Services.AddScoped<IUxNewPlayer<Player>, SocketUxNewPlayer>();
builder.Services.AddScoped<IUxEnrollTotp<Player, Player>, SocketUxEnrollTotp>();
builder.Services.AddScoped<IUxKnownPlayer<Player, string>, SocketUxKnownPlayer>();
builder.Services.AddScoped<IUxGameLoop<Player, Player>, SocketUxGameLoop>();
builder.Services.AddScoped<IUxChallengeTotp<bool, Player>, SocketUxTotpChallenge>();
builder.Services.AddScoped<IDal<Body>, DynamoDb<Body>>();
builder.Services.AddScoped<IDal<Player>, DynamoDb<Player>>();

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

            var handler = scope.ServiceProvider.GetRequiredService<IUxLogin<Player>>();

            await handler.HandleUx(socket);
        }
    }
    else
    {
        await next();
    }
});

app.Run("http://100.115.92.204:5260");
