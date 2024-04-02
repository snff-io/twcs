using System.Drawing;
using System.Runtime.InteropServices;
using library.worldcomputer.info;
using OtpNet;
using QRCoder.Exceptions;

public class SocketUxTotpChallenge : IUxChallengeTotp<bool, IUnit>
{
    private ITotp _totp;
    private IDal<Body> _bodyDal;

    public SocketUxTotpChallenge(ITotp totp, IDal<Body> bodyDal)
    {
        _totp = totp;
        _bodyDal = bodyDal;
    }
    public async Task<bool> HandleUx( Socket socket, IUnit unit)
    {
        await "Enter the 6 digits from your authentictor:".Text().Send(socket);

        var tries = 10;
        while (tries > 0)
        {
            tries--;

            var secRemain = _totp.RemainingSeconds(unit.Secret);
            
            var value = await socket.PromptForRx($": ", "\\d{6}" );
            var valid = _totp.ValidateTotp(unit.Secret, value);
            
            if (valid) 
            {
                await $"Success! Saving...".Text().Send(socket);   
                await "\n".Send(socket);
                await $"You are : {unit.FirstName} {unit.LastName}".Info().Send(socket);             
                unit.Bound = true;
                unit.LastLogin = DateTime.Now;
                await _bodyDal.Put((Body)unit);

                return true;
            }
            else
            {
                await $"invalid otp, {tries.ToString()} remaining)".Error().Send(socket);
            }

        }

        return false;

    }
}
