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
        await "Enter the 6 digits from your authentictor:".Send(socket);

        var tries = 10;
        while (tries > 0)
        {
            tries--;

            var secRemain = _totp.RemainingSeconds(unit.Secret);
            
            var value = await socket.PromptForRx($"{secRemain} seconds: ", "\\d{6}" );
            var valid = _totp.ValidateTotp(unit.Secret, value);
            
            if (valid) 
            {
                await $"success! saving...".Send(socket);   
                await $"you are : {unit.FirstName} {unit.LastName}".Color(System.Drawing.KnownColor.Yellow).Send(socket);             
                unit.Bound = true;
                unit.LastLogin = DateTime.Now;
                await _bodyDal.Put((Body)unit);

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
