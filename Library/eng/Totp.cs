using System.Text.RegularExpressions;
using OtpNet;

namespace library.worldcomputer.info;

public class TotpWrapper : ITotp
{

    public TotpWrapper()
    {

    }


    public string GenerateSecret()
    {
        var bytes = KeyGeneration.GenerateRandomKey();
        return Base32Encoding.ToString(bytes);
    }

    public string GenerateTotp(string secret)
    {
        var se = Base32Encoding.ToBytes(secret);
        var to = new Totp(se);

        return to.ComputeTotp();
    }

    public bool ValidateTotp(string secret, string value)
    {
        var se = Base32Encoding.ToBytes(secret);
        var to = new Totp(se);
        long timeStepMatched;
        return to.VerifyTotp(value, out timeStepMatched);

    }

    public int RemainingSeconds(string secret)
    {
        var se = Base32Encoding.ToBytes(secret);
        var to = new Totp(se);

        return to.RemainingSeconds();

    }

    public string GenerateQrCode(string secret, string user, string issuer)
    {
        var data = $"otpauth://totp/{user}?secret={secret}&issuer={issuer}";
        return Ansi.GenerateQRCode(data);
    }
}

