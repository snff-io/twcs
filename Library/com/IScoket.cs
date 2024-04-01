using library.worldcomputer.info;

public interface ISocket
{
    SocketState State { get; }
    Task SendAsync(string message, Header mHeader = Header.Text, string mHeaderArg = "");
    Task<string> ReceiveAsync();
    Task CloseAsync();
}

public enum Header
{
    None,
    Text,
    Image,
    Location
}

public enum SocketState
{
    //
    // Summary:
    //     Reserved for future use.
    None = 0,
    //
    // Summary:
    //     The connection is negotiating the handshake with the remote endpoint.
    Connecting = 1,
    //
    // Summary:
    //     The initial state after the HTTP handshake has been completed.
    Open = 2,
    //
    // Summary:
    //     A close message was sent to the remote endpoint.
    CloseSent = 3,
    //
    // Summary:
    //     A close message was received from the remote endpoint.
    CloseReceived = 4,
    //
    // Summary:
    //     Indicates the WebSocket close handshake completed gracefully.
    Closed = 5,
    //
    // Summary:
    //     Indicates that the WebSocket has been aborted.
    Aborted = 6
}