
using Xunit;
using library.worldcomputer.info;
using Microsoft.OpenApi.Expressions;
using System.Net.WebSockets;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using terminal.worldcomputer.info;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace test.worldcomputer.info;

public class WebSocketMock : WebSocket
{
    public WebSocketMock()
    {
       
    }

    public override WebSocketCloseStatus? CloseStatus => throw new NotImplementedException();

    public override string? CloseStatusDescription => throw new NotImplementedException();

    public override WebSocketState State => throw new NotImplementedException();

    public override string? SubProtocol => throw new NotImplementedException();

    public override void Abort()
    {
        throw new NotImplementedException();
    }

    public override Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override Task CloseOutputAsync(WebSocketCloseStatus closeStatus, string? statusDescription, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }

    public override Task<WebSocketReceiveResult> ReceiveAsync(ArraySegment<byte> buffer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}



public class DalMock<T> : Mock<IDal<T>> where T : class
{
    public DalMock()
    {
        SetupAll();
    }

    private void SetupAll()
    {
        Setup(x => x.Put(It.IsAny<T>()))
            .Returns(Task.CompletedTask);

        Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((string table, string key) => null); // Adjust this to return a specific result if needed
    }
}
public interface IWebHostEnvironment 
{
    string WebRootPath { get; set; }

}

public class WebHostEnvironmentMock :Microsoft.AspNetCore.Hosting.IWebHostEnvironment
{
    public string WebRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IFileProvider WebRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string ContentRootPath { get => "/home/zampinojosh/src/twcs/Terminal/static"; set => throw new NotImplementedException(); }
    public string EnvironmentName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}


public class Test_NewPlayerUx
{


    [Fact]
    public async void Says_Welcome()
    {
        ///var npux = new SocketNewPlayerUx((IDal<Body>)new DalMock<Body>(), (IDal<Player>) new DalMock<Player>(), new WebSocketMock(), new WebHostEnvironmentMock());
        
        


    }

}