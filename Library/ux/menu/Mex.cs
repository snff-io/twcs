using System.Drawing;

namespace library.worldcomputer.info;

public static class Mex
{

    public static string Header(this string value)
    {
        var v = value.Color(KnownColor.CornflowerBlue).Pre(">_");
        v += new string('=', value.Length).Color(KnownColor.CornflowerBlue).Pre(">_"); ;

        return v;
    }

    public async static Task<int> Menu(this string name, ISocket socket, params string[] choices)
    {
        await name.Header().Send(socket);
        var cnt = 1;
        for (cnt = 1; cnt < choices.Count(); cnt++)
        {
            await ($"{cnt}) " + choices[cnt-1].Option()).Send(socket);
        }

        var choice = await socket.PromptForRx($"\n[1-{cnt}]:".Prompt(), $"[1-{cnt}]");

        return int.Parse(choice);
    }
}