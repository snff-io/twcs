public interface ITotp
{
    public string GenerateSecret();

    public string GenerateTotp(string secret);

    public bool ValidateTotp(string secret, string value);

    public int RemainingSeconds(string secret);

    public string GenerateQrCode(string secret, string user, string issuer);


}