using System.Runtime.InteropServices;
using library.worldcomputer.info;
using OtpNet;
using QRCoder.Exceptions;

public class SocketUxTotpChallenge : IUxChallengeTotp<bool, Player>
{
    private ITotp _totp;
    private IDal<Player> _playerDal;
    private IDal<Body> _bodyDal;

    public SocketUxTotpChallenge(ITotp totp, IDal<Player> playerDal, IDal<Body> bodyDal)
    {
        _totp = totp;
        _playerDal = playerDal;
        _bodyDal = bodyDal;
    }
    public async Task<bool> HandleUx( Socket socket, Player player)
    {
        await "Enter the 6 digits from your authentictor:".Send(socket);

        var tries = 10;
        while (tries > 0)
        {
            tries--;

            var secRemain = _totp.RemainingSeconds(player.Id);
            
            var value = await socket.PromptForRx($"{secRemain} seconds: ", "\\d{6}" );
            var valid = _totp.ValidateTotp(player.Id, value);
            
            if (valid) 
            {
                await $"success! saving...".Send(socket);
                
                var body = await _bodyDal.Get(player.Chosen);
                body.Bound = true;

                await _bodyDal.Put(body);

                player.LoggedIn = DateTime.Now;
                await _playerDal.Put(player);


                return true;
            }
            else
            {
                await $"invalid otp, {tries.ToString()} remaining)".Send(socket);
            }

        }

        return false;

    }
}
