namespace library.worldcomputer.info;
using System.Drawing;
using System.Net.Sockets;

public static class Sx
{
    public async static Task Send(this string message, Socket socket)
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
        return value.Color(KnownColor.Orange).Pre(">_");
    }

    public static string Emph(this string value)
    {
        return value.Color(KnownColor.CornflowerBlue).Pre("#_");
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
        return value.Color(KnownColor.Orange).Pre(")_");
    }

    public static string Prompt(this string value)
    {
        return value.Color(KnownColor.Orange).Pre("?_");
    }
}