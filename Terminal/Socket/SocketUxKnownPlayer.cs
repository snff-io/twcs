using System.Drawing;
using library.worldcomputer.info;

public class SocketUxKnownPlayer : IUxKnownPlayer<IUnit, string>
{
    private IDal<Body> _bodyDal;

    public SocketUxKnownPlayer(IDal<Body> bodyDal)
    {
        _bodyDal = bodyDal;
    }

    public async Task<IUnit> HandleUx(Socket socket, string unit)
    {
        await $"Looking for {unit}, one moment please.,,".Text().Send(socket);

        var body = await _bodyDal.Get(IUnit.ToId(unit));

        //player nor body exist
        if (body == null)
        {
            await "I don't know anyone by that name.".Error().Send(socket);
            return null;
        }
        else
        {
            await $"found them.".Text().Send(socket);
            return body;
        }
    }
}