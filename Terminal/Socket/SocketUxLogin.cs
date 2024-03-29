using System.Net.WebSockets;
using library.worldcomputer.info;
using System.Text;


public class SocketUxLogin : IUxLogin<Player>
{
    private IWebHostEnvironment _env;
    private IWordResolver _wordResolver;
    private IUxNewPlayer<Player> _newPlayerUx;
    private IUxEnrollTotp<Player, Player> _enrollTotpUx;
    private IUxChallengeTotp<bool, Player> _challengeTotp;
    private IUxGameLoop<Player, Player> _gameLoopUx;
    private IUxKnownPlayer<Player, string> _knownPlayerUx;

    public SocketUxLogin(IWebHostEnvironment env, IWordResolver wordResolver,
      IUxNewPlayer<Player> newPlayerUx,
      IUxEnrollTotp<Player, Player> enrollTotpUx,
      IUxKnownPlayer<Player, string> knownPlayerUx,
      IUxGameLoop<Player, Player> gameLoopUx,
      IUxChallengeTotp<bool, Player> challengeTotpUx
       )
    {
        _env = env;
        _wordResolver = wordResolver;
        _newPlayerUx = newPlayerUx;
        _enrollTotpUx = enrollTotpUx;
        _challengeTotp = challengeTotpUx;
        _gameLoopUx = gameLoopUx;
        _knownPlayerUx = knownPlayerUx;
    }

    public async Task<Player> HandleUx(Socket socket)
    {
        var input = "";
        Player player = new Player();

        try
        {
            string filePath = Path.Combine(_env.ContentRootPath, "static/login.ans");
            await socket.SendAsync(File.ReadAllText(filePath));
            await socket.SendAsync("Hello, who are you? New?: ");
            input = await socket.ReceiveAsync();
        }
        catch
        {
            throw new Exception("Socket problems...");
        }

        var isNew = _wordResolver.Resolve(input, PartOfSpeech.adj, "new");

        if (input.Split(' ').Count() != 2 && isNew != null && isNew != "" && isNew == "new")
        {
            player = await _newPlayerUx.HandleUx(socket);
        }
        else
        {
            player = await _knownPlayerUx.HandleUx(socket, input);
            if (player == null)
            {
                player = await _newPlayerUx.HandleUx(socket);
            }
        }

        if (player.Id == "" || player.Id == null)
        {
            player = await _enrollTotpUx.HandleUx(socket, player);
        }

        try
        {
            var sucess = await _challengeTotp.HandleUx(socket, player);

            if (sucess)
            {
                return await _gameLoopUx.HandleUx(socket, player);
            }
            else
            {
                await "Login failed. :(".Send(socket);
                await "Send mail to support@worldcomputer.info for assistance!".Send(socket);
            }
        }
        finally
        {
            await socket.CloseAsync();
        }

        return player;

    }

}