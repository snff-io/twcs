using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MongoDB.Bson.IO;


public class AnsiImageHandler : IImageHandler
{
    Dictionary<string, string> _map;
    private string _server;

    public AnsiImageHandler(string server = "https://worldcomputer.info/pic/")
    {
        _server = server;


        _map = new Dictionary<string, string>{
            {"login", "energy/_8443df866856aabec61d8eaccf41953974ec9191.jpeg"},
            {"newuser1", "people/_203cbeedc14a19ea5e29ff2ac9587ed25730e1ee.jpeg"},
            {"newuser2", "people/_2bb67998d261e7402f73bd3d5fdad1ec8208860b.jpeg"},
            {"welcome", "hash"},
        };
    }

    public async Task<string> GetMappedAnsi(string map)
    {
        var hc = new HttpClient();
        var path = _map[map].Replace("_", "ans/_").Replace(".jpeg",".ans");
        var uri = _server + path;

        var ansi = await hc.GetStringAsync(_server + path);

        ansi = ("\n" + _server + _map[map] + "\n").Color(System.Drawing.KnownColor.Black) + ansi;

        return ansi;
    }

    public async Task<byte[]> GetMappedImage(string map)
    {
        var hc  = new HttpClient();
        var image = await hc.GetByteArrayAsync(_server + _map[map]);
        return image;
    }

}

