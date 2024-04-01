using library.worldcomputer.info;

public class SocketUxGameLoop:IUxGameLoop<IUnit, IUnit>
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

    public async Task<IUnit> HandleUx(Socket socket, IUnit unit)
    {
        string filePath = Path.Combine(_env.ContentRootPath, "static/welcome.ans");
        
        await socket.SendAsync(File.ReadAllText(filePath));
        while (socket.State == SocketState.Open)
        {
            await "Silence echoes through the void, defining an infinite vista of emptiness.".Send(socket);
            var input = await socket.ReceiveAsync();

            var output = _cmdParser.ParseCommand(input);

            await socket.SendAsync(output);
        }

        await socket.CloseAsync();

        return unit;
    }
}
