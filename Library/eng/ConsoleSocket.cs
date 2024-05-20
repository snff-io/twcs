
namespace library.worldcomputer.info;


public class ConsoleSocket : ISocket
{
    public SocketState State => throw new NotImplementedException();

    public Task CloseAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<string> PromptForRx(string prompt, string regex)
    {
        return "";
    }

    public Task<string> ReceiveAsync()
    {
        throw new NotImplementedException();
    }

    public async Task SendAsync(string message)
    {
        Console.WriteLine(message);
    }
}