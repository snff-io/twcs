using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

public class InputProcessorWebSocket
{

    ICmdParser _cmdParser;
    public InputProcessorWebSocket(ICmdParser cmdParser)
    {
        _cmdParser = cmdParser;
    }

    public async Task HandleWebSocket(HttpContext context, WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
            var input = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
            // Resolve the input using IInput.Resolve method
            // You need to inject IInput into this class or use a service locator
            var output = _cmdParser.ParseCommand(input);
            // Send back the result to the client
           
            var outputBuffer = System.Text.Encoding.UTF8.GetBytes(output);
            await webSocket.SendAsync(new ArraySegment<byte>(outputBuffer, 0, outputBuffer.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
}
