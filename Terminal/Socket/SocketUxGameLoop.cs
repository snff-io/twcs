using library.worldcomputer.info;

public class SocketUxGameLoop:IUxGameLoop<Player, Player>
{

    ICmdParser _cmdParser;
    private IWebHostEnvironment _env;
    private IWordResolver _wordResolver;

    public SocketUxGameLoop(ICmdParser cmdParser, IWebHostEnvironment env, IWordResolver wordResolver)
    {
        _cmdParser = cmdParser;
        _env = env;
        _wordResolver = wordResolver;
    }

    public async Task<Player> HandleUx(Socket socket, Player player)
    {
        string filePath = Path.Combine(_env.ContentRootPath, "static/welcome.ans");
        
        await socket.SendAsync(File.ReadAllText(filePath));
        while (socket.State == SocketState.Open)
        {
            var input = await socket.ReceiveAsync();

            var output = _cmdParser.ParseCommand(input);

            await socket.SendAsync(output);
        }

        await socket.CloseAsync();

        return player;
    }
}
