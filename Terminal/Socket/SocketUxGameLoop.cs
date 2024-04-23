using System.Globalization;
using System;
using library.worldcomputer.info;

public class SocketUxGameLoop : IUxGameLoop<IUnit, IUnit>
{

    private IWebHostEnvironment _env;
    private IWordResolver _wordResolver;
    private IImageHandler _image;
    private IServiceProvider _serviceProvider;

    public SocketUxGameLoop(IWebHostEnvironment env, IWordResolver wordResolver, IImageHandler imageHandler, IServiceProvider serviceProvider)
    {
        _env = env;
        _wordResolver = wordResolver;
        _image = imageHandler;
        _serviceProvider = serviceProvider;
    }

    public async Task<IUnit> HandleUx(Socket socket, IUnit unit)
    {
        await "[wecome / motd / etc]".Text().Send(socket);

        var intents = _serviceProvider.GetServices<IIntent>();
        var intentActions = _serviceProvider.GetServices<IIntentAction>();

        var locationIntentAction = intentActions.Where(x=>x.Intent == "location").FirstOrDefault();

        if (locationIntentAction == null)
        {
            throw new Exception("Problem with location intent action!");
        }

         await locationIntentAction.Exec("location", unit, socket);

        while (socket.State == SocketState.Open)
        {
            await "Main".Emph().Send(socket);
           
            var input = await socket.ReceiveAsync();

            foreach (var intent in intents)
            {
                var tpr = await intent.TryParse(input);

                if (tpr.Success)
                {
                    var ia = intentActions.Where(x => x.Intent == tpr.Intent).FirstOrDefault();

                    if (ia == null)
                    {
                        var ilist = string.Join(", ", intentActions.Select(x => x.Intent));
                        await $"That action seems unavailable. Try {ilist}".Error().Send(socket);
                        continue;
                    }
                    await tpr.IntentPath.Emph().Send(socket);
                    var iar = await ia.Exec(tpr.IntentPath, unit, socket);

                    if (iar.Success && iar.Next == "location")
                    {
                        await locationIntentAction.Exec("location", unit, socket);
                    }
                }
            }
        }

        await socket.CloseAsync();

        return unit;
    }
}
