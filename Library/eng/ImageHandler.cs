using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using library.worldcomputer.info;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MongoDB.Bson.IO;


public class AnsiImageHandler : IImageHandler
{
    Dictionary<string, string> _map;
    private HttpClient _client;
    private string _server;



    public AnsiImageHandler(HttpClient client, string server = "https://worldcomputer.info/pic/")
    {
        _client = client;
        _server = server;


        _map = new Dictionary<string, string>{
            {"login", "energy/_8443df866856aabec61d8eaccf41953974ec9191.jpeg"},
            {"newuser1", "people/_203cbeedc14a19ea5e29ff2ac9587ed25730e1ee.jpeg"},
            {"newuser2", "people/_2bb67998d261e7402f73bd3d5fdad1ec8208860b.jpeg"},
            {"covert_biclighter", "tech/_7c7e2217c3bb13defa46c0fc780bf8b89803c614.jpeg"},
            {"reflective_truckerhat", "tech/_89d50fd42bf2dc39ea068646d8be84bba819c386.jpeg"},
            {"default_item", "tech/_3ff00a406694b6d2640d595d370306acab1dd9d4ljpeg"},
            {"welcome", "hash"},
        };
    }

    public Image this[string map] => GetNamedImage(map);


    public Image GetNamedImage(string map)
    {

        var ansipath = _server + _map[map].Replace("_", "ans/_").Replace(".jpeg", ".ans");
        var imagepath = _server + _map[map];

        return new Image(ansipath, imagepath, _client);
    }

    public async Task<List<Image>> GetLandscapeImages(Domain top, Domain bottom)
    {
        var indexData = await _client.GetStringAsync($"{_server}landscapes/index.txt");
        var index = indexData.Split('\n');

        var domainPath = $"./{top}_{bottom}/".ToLower();
        var domainIndex = index.Where(x => x.StartsWith(domainPath));
        // Group file paths by filename without extension
        var groupedFiles = domainIndex.GroupBy(path =>
            Path.GetFileNameWithoutExtension(path));

        // Create Image objects from grouped files
        List<Image> images = new List<Image>();
        foreach (var group in groupedFiles)
        {
            string? ansPath = group.FirstOrDefault(p => p.EndsWith(".ans"));
            string? jpegPath = group.FirstOrDefault(p => p.EndsWith(".jpeg"));

            if (ansPath != null && jpegPath != null)
            {
                ansPath = ansPath.Replace("./", $"{_server}landscapes/");
                jpegPath = jpegPath.Replace("./", $"{_server}landscapes/");

                images.Add(new Image(ansPath, jpegPath, _client));
            }
        }
        return images;
    }

    public async Task<Image> GetRandomLandscape(Domain top, Domain bottom)
    {
        var list = await GetLandscapeImages(top, bottom);

        var rnd = new Random((int)top + (int)bottom);
        var r = rnd.Next(0, list.Count() - 1);

        return list[r];
    }
}

