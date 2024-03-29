using library.worldcomputer.info;

public class SocketUxKnownPlayer : IUxKnownPlayer<Player, string>
{
    private IDal<Player> _playerDal;
    private IDal<Body> _bodyDal;

    public SocketUxKnownPlayer(IDal<Player> playerDal, IDal<Body> bodyDal)
    {
        _playerDal = playerDal;
        _bodyDal = bodyDal;

    }

    public async Task<Player> HandleUx(Socket socket, string unit)
    {

        await $"Looking for {unit}, one moment please.,,".Send(socket);

        var player = await _playerDal.GetByAttr("Chosen", IUnit.ToId(unit));
        var body = await _bodyDal.Get(IUnit.ToId(unit));

        //player nor body exist
        if (player == null && body == null)
        {
            await "I don't know anyone by that name.".Send(socket);
            return null;
        }

        //no player binding
        if (player == null && body != null)
        {
            await $"I found {unit}, you might be able to have that vessel in time... not today though.".Send(socket);
            return null;
        }

        if (player != null && body != null)
        {
            await $"found them.".Send(socket);
            return player;
        }

        return null;

    }
}