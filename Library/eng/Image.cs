using System.Drawing;

namespace library.worldcomputer.info;

public class Image
{
    public string AnsiUri { get; set; }

    public string JpegUri { get; set; }

    HttpClient _httpClient;

    string _ansiString = "";

    byte[]? _imageBytes = null;
    public async Task<string> GetAnsiString()
    {
        if (_ansiString == "")
        {
            _ansiString = await _httpClient.GetStringAsync(AnsiUri);
        }
        return _ansiString;
    }

    public async Task<byte[]> GetImageBytes()
    {
        if (_imageBytes == null)
        {
            _imageBytes = await _httpClient.GetByteArrayAsync(JpegUri);
        }
        return _imageBytes;
    }

    public Image(string ansiUri, string jpegUri, HttpClient client)
    {
        AnsiUri = ansiUri;
        JpegUri = jpegUri;
        _httpClient = client;
    }

}

public static class Ix
{
    public async static Task Send(this Image image, Socket socket)
    {
        var ansiString = await image.GetAnsiString();
        await image.JpegUri.Jpeg().Color(KnownColor.Black).Send(socket);
        await ansiString.Ansi().Send(socket);
    }

}