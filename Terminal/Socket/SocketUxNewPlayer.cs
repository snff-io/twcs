using System.Drawing;
using System.Globalization;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using library.worldcomputer.info;
namespace terminal.worldcomputer.info;
public class SocketUxNewPlayer : IUxNewPlayer<IUnit>
{
    private IDal<Body> _bodyDal;
    private WebSocket _socket;
    private IWebHostEnvironment _env;
    private IImageHandler _image;
    private IGrid _grid;

    public SocketUxNewPlayer(IDal<Body> bodyDal, IWebHostEnvironment env, IImageHandler imageHandler, IGrid grid)
    {
        _bodyDal = bodyDal;
        _env = env;
        _image = imageHandler;
        _grid = grid;
    }

    public async Task<IUnit> HandleUx(Socket socket)
    {
        await _image["newuser1"].Send(socket);
        await "You're about to embark on an exciting journey!".Emph().Text().Send(socket);
        await "Before you dive in, you have the unique opportunity to choose".Text().Send(socket);
        await "the body that will be your vessel throughout this adventure.".Text().Send(socket);
        await "To Åcend, or Æpher, spiritus, spiritum - zamja circa 2024\n".Text().Send(socket);

        var blist = await _bodyDal.GetRandomUnbound();

        for (var i = 0; i < blist.Length; i++)
        {
            await $"{i + 1}. {blist[i].FirstName}, {blist[i].LastName}".Info().Send(socket);
        }

        var choice = await socket.PromptForRx("\nYour choice? [1-5]:".Pre("?_"), "[1-5]");

        var chint = int.Parse(choice) - 1;

        var player = (IUnit)blist[chint];

        var pg = _grid.GetRandomPoistion(0);
        player.Location.Layer = 0;
        player.Location.X = pg.Current.X;
        player.Location.Y = pg.Current.Y;
        (player as Body).Suit = new BodySuit(player);
        (player as Body).Suit.New();

        await $"\n\n{player.FirstName} {player.LastName}\n\n".Info().Send(socket);
        await $"Remember your name, you'll need it to login!".Emph().Send(socket);
        await "\n".Send(socket);
        await $"Let's continue your registration...".Text().Send(socket);

        return player;
    }
}