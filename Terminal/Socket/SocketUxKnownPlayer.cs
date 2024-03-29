using library.worldcomputer.info;

public class SocketUxKnownPlayer : IUxKnownPlayer<Player>
{

    public async Task<Player> HandleUx(Socket socket)
    {
        await socket.SendAsync("KnownPlayer");
        return new Player();
    }
}