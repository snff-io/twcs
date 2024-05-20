namespace library.worldcomputer.info;
using System.Drawing;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using DZen.BasE91;

public static class Sx
{
    public async static Task Send(this string message, ISocket socket)
    {
        await socket.SendAsync(message);
    }

    public static string Pre(this string value, string prefix)
    {
        return prefix.Color(KnownColor.Black) + value;
    }

    public static string Text(this string value, KnownColor knownColor = KnownColor.Gainsboro)
    {
        return value.Color(knownColor).Pre("._");
    }

    public static string Info(this string value)
    {
        return value.Color(KnownColor.Orange).Pre("<_");
    }

    public static string Emph(this string value)
    {
        return value.Color(KnownColor.CornflowerBlue).Pre("*_");
    }

    public static string Ansi(this string value)
    {
        return value.Pre("~_\n");
    }
    public static string Jpeg(this string value)
    {
        return value.Color(KnownColor.Black).Pre("-_\n");
    }

    public static string Chat(this string value)
    {
        return value.Pre("@_");
    }

    public static string Error(this string value)
    {
        return value.Pre("!_").Color(KnownColor.IndianRed);
    }


    public static string Option(this string value)
    {
        return value.Color(KnownColor.Orange).Pre("(_");
    }

    public static string Prompt(this string value)
    {
        return value.Color(KnownColor.Orange).Pre("?_");
    }

    public static async Task Lf(this Socket socket)
    {
        await "\n".Send(socket);
    }

    public static string Hex(this string value)
    {
        char[] chars = value.ToCharArray();
        string hexOutput = "";
        foreach (char c in chars)
        {
            // Convert each character to its hexadecimal representation
            hexOutput += String.Format("{0:X2}", (int)c);
        }
        return hexOutput;
    }

    public static string Sha1(this string value, int take = 5)
    {
        using (SHA1 sha1 = SHA1.Create())
        {
            // Convert the input string to a byte array and compute the hash
            byte[] inputBytes = Encoding.UTF8.GetBytes(value);
            byte[] hashBytes = sha1.ComputeHash(inputBytes);

            // Convert the byte array to a hexadecimal string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                // Convert each byte to a Hexadecimal string
                sb.Append(b.ToString("x2"));
            }

            if (take == -1)
            {
                return sb.ToString();
            }

            return sb.ToString()[..take];
        }
    }

    public static string Hex(this float value)
    {

        byte[] bytes = BitConverter.GetBytes(value);

        // Convert the byte array to a hex string
        string hex = BitConverter.ToString(bytes).Replace("-", "");

        return hex;
    }

    public static int ToDec(this string value)
    {

        int sum = 0;
        foreach (char c in value)
        {
            sum += c;
        }
        return sum;
    }

    public static float ToAvg(this string value)
    {
        int sum = 0;
        int cnt = 0;
        foreach (char c in value)
        {
            cnt++;
            sum += c;
        }
        return sum / cnt;
    }

    public static string b91(this string value)
    {
        return BasE91.Encode(Encoding.UTF8.GetBytes(value)).ToString();
    }
}