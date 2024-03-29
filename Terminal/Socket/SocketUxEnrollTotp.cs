using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using library.worldcomputer.info;
using OtpNet;
using QRCoder.Exceptions;
namespace terminal.worldcomputer.info;
public class SocketUxEnrollTotp : IUxEnrollTotp<Player,Player>
{
    private readonly ITotp _totp;
    private readonly IDal<Player> _dal;

    public SocketUxEnrollTotp(ITotp totp, IDal<Player> dal)
    {
        _totp = totp;
        _dal = dal;
    }
    public async Task<Player> HandleUx(Socket socket, Player player)
    {
        await "For simplicity and enhanced security, we exclusively utilize one-time passwords.".Send(socket);
        await "They serve as both a safeguard and a form of captcha. To access, you'll require an".Send(socket);
        await "authenticator app capable of scanning a QR code. Stay protected!\n".Send(socket);
        await "Let's setup your Authenticator App\n\n".Send(socket);
        await "Scan this code with your authenticator app...".Send(socket);


        player.Id = _totp.GenerateSecret();
        var data = _totp.GenerateQrCode(player.Id, player.Chosen, "worldcomputer.info");
        await data.Send(socket);
        

        await socket.PromptForRx("Press enter to continue. . .", ".*");
        await Ansi.Clear.Send(socket);
        
        return player;
    }

}