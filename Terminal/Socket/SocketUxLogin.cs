using System.Drawing;
using System.Globalization;
using library.worldcomputer.info;


public class SocketUxLogin : IUxLogin<IUnit>
{
    private IWebHostEnvironment _env;
    private IWordResolver _wordResolver;
    private IUxNewPlayer<IUnit> _newPlayerUx;
    private IUxEnrollTotp<IUnit, IUnit> _enrollTotpUx;
    private IUxChallengeTotp<bool, IUnit> _challengeTotp;
    private IUxGameLoop<IUnit, IUnit> _gameLoopUx;
    private IUxKnownPlayer<IUnit, string> _knownPlayerUx;
    private IImageHandler _image;

    public SocketUxLogin(IWebHostEnvironment env, IWordResolver wordResolver,
      IUxNewPlayer<IUnit> newPlayerUx,
      IUxEnrollTotp<IUnit, IUnit> enrollTotpUx,
      IUxKnownPlayer<IUnit, string> knownPlayerUx,
      IUxGameLoop<IUnit, IUnit> gameLoopUx,
      IUxChallengeTotp<bool, IUnit> challengeTotpUx,
      IImageHandler imageHandler
       )
    {
        _env = env;
        _wordResolver = wordResolver;
        _newPlayerUx = newPlayerUx;
        _enrollTotpUx = enrollTotpUx;
        _challengeTotp = challengeTotpUx;
        _gameLoopUx = gameLoopUx;
        _knownPlayerUx = knownPlayerUx;
        _image = imageHandler;
    }

    public async Task<IUnit> HandleUx(Socket socket)
    {
        var input = "";
        IUnit unit;
        try
        {
            await _image["login"].Send(socket);
            await "Hello, who are you? New?: ".Text().Send(socket);

            input = await socket.ReceiveAsync();
        }
        catch
        {
            throw new Exception("Socket problems...");
        }

        var isNew = await _wordResolver.Resolve(input, PartOfSpeech.adj, "new");

        if (input.Split(' ').Count() != 2 && isNew != null && isNew != "" && isNew == "new")
        {
            unit = await _newPlayerUx.HandleUx(socket);
        }
        else
        {
            unit = await _knownPlayerUx.HandleUx(socket, input);
            if (unit == null)
            {
                unit = await _newPlayerUx.HandleUx(socket);
            }
        }

        if (unit.Secret == "" || unit.Secret == null)
        {
            unit = await _enrollTotpUx.HandleUx(socket, unit);
        }

        try
        {
            var success = await _challengeTotp.HandleUx(socket, unit);

            if (success)
            {
                return await _gameLoopUx.HandleUx(socket, unit);
            }
            else
            {
                await "Login failed. :(".Error().Send(socket);
                await "Send mail to support@worldcomputer.info for assistance!".Text().Send(socket);
            }
        }
        finally
        {
            await socket.CloseAsync();
        }

        return unit;

    }

}