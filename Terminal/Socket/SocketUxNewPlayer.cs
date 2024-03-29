using System.Drawing;
using System.Globalization;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using library.worldcomputer.info;
namespace terminal.worldcomputer.info;
public class SocketUxNewPlayer : IUxNewPlayer<Player>
{
    private IDal<Body> _bodyDal;
    private IDal<Player> _playerDal;
    private WebSocket _socket;
    private IWebHostEnvironment _env;

    public SocketUxNewPlayer(IDal<Body> bodyDal, IDal<Player> playerDal, IWebHostEnvironment env)
    {
        _bodyDal = bodyDal;
        _playerDal = playerDal;
        _env = env;
    }

    public async Task<Player> HandleUx(Socket socket)
    {
        await File.ReadAllText(Path.Combine(_env.ContentRootPath, "static/aepherum_male.ans")).Send(socket);
        await File.ReadAllText(Path.Combine(_env.ContentRootPath, "static/aepherum_female.ans")).Send(socket);

        await "\nYou're about to embark on an exciting journey!".Send(socket);        
        await "Before you dive in, you have the unique opportunity to choose".Send(socket);
        await "the body that will be your vessel throughout this adventure.".Send(socket);
        await "To Åcend, or Æpher, spiritus, spiritum - zamja circa 2024\n".Send(socket);

        var blist = await _bodyDal.GetRandomUnbound();

        for (var i = 0; i < blist.Length; i++)
        {
            await $"{i+1}. {blist[i].FirstName}, {blist[i].LastName}".Send(socket);
        }

        var choice = await socket.PromptForRx("\nYour choice? [1-5]:", "[1-5]");

        var chint = int.Parse(choice) - 1;

        var player = new Player();
        player.Chosen = blist[chint].Id;

        await $"\n\nRemember your Chosen's name, you'll need it to login!".Send(socket);
        await $"{blist[chint].FirstName} {blist[chint].LastName}".Color(KnownColor.Yellow).Send(socket);
        await $"let's continue your registration...".Send(socket);

        return player;
    }
}