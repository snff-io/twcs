using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using library.worldcomputer.info;
namespace terminal.worldcomputer.info;
public class SocketUxEnrollTotp : IUxEnrollTotp<Player,Player>
{
    public async Task<Player> HandleUx(Socket socket, Player unit)
    {
        await socket.SendAsync("Enroll OTP");

        await AnsiHelper.GenerateQRCode("hello qr").Send(socket);

        return unit;
    }

}