using System.Drawing;
using library.worldcomputer.info;
namespace terminal.worldcomputer.info;
public class SocketUxEnrollTotp : IUxEnrollTotp<IUnit,IUnit>
{
    private readonly ITotp _totp;
    private readonly IDal<Body> _dal;

    public SocketUxEnrollTotp(ITotp totp, IDal<Body> dal)
    {
        _totp = totp;
        _dal = dal;
    }

    public async Task<IUnit> HandleUx(Socket socket, IUnit unit)
    {
        await "For simplicity and enhanced security, we exclusively utilize one-time passwords.".Text().Send(socket);
        await "They serve as both a safeguard and a form of captcha. To access, you'll require an".Text().Send(socket);
        await "authenticator app capable of scanning a QR code. Stay protected!\n".Text().Send(socket);
        await "Let's setup your Authenticator App\n\n".Text().Send(socket);
        await "Scan this code with your authenticator app...".Emph().Send(socket);


        unit.Secret = _totp.GenerateSecret();
        var data = _totp.GenerateQrCode(unit.Secret, $"{unit.FirstName} {unit.LastName}", "bltwc.io");
        await data.Send(socket);
        

        await socket.PromptForRx("Press enter to continue. . .".Text(), ".*");
        await Ansi.Clear.Send(socket);
        
        return unit;
    }

}