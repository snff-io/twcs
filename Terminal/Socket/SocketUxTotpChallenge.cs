using library.worldcomputer.info;

public class SocketUxTotpChallenge : IUxChallengeTotp<bool, Player>
{
    public async Task<bool> HandleUx(Socket socket, Player unit)
    {
        await socket.SendAsync("TOTP Challenge");
        return true;
    }
}
