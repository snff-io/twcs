namespace library.worldcomputer.info;

using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Builder;
using System.Text.RegularExpressions;
using Amazon.Runtime.Credentials.Internal;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

public class Socket : ISocket
{
    private readonly WebSocket _webSocket;
    private readonly IWordResolver _wordResolver;

    public Socket(WebSocket webSocket, IWordResolver wordResolver)
    {
        _webSocket = webSocket;
        _wordResolver = wordResolver;
    }

    public SocketState State
    {
        get
        {
            return (SocketState)_webSocket.State;
        }
    }

    public async Task SendAsync(string message)
    {
        var buffer = System.Text.Encoding.UTF8.GetBytes(message);
        await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
    }

    public async Task<string> ReceiveAsync()
    {
        var buffer = new byte[1024];
        var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        return System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
    }

    public async Task CloseAsync()
    {
        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        _webSocket.Dispose();
    }

    public async Task<string> PromptForRx(string prompt, string regex)
    {
        var rx = new Regex(regex);
        var matched = false;
        var response = "deadfood";
        while (!matched)
        {
            await SendAsync(prompt);

            response = await ReceiveAsync();
            if (rx.IsMatch(response))
            {
                matched = true;
                break;
            }
        }
        return response;
    }

    public async Task<string> PromptForWord(string prompt, PartOfSpeech pos, params string[] words)
    {
        var matched = false;
        var response = "deadfood";
        while (!matched)
        {
            await SendAsync(prompt);
            response = await ReceiveAsync();

            string word = await _wordResolver.Resolve(response, pos, words);

            if (word != null && word != "" && words.Contains(word))
            {
                matched = true;
                break;
            }
        }
        return response;
    }
}


