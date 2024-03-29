using System.Net.WebSockets;
using library.worldcomputer.info;
using Microsoft.AspNetCore.Identity;

public interface IUx<TReturn>
{
    
    Task<TReturn> HandleUx(Socket socket);

}


public interface IUx<TReturn, T>
{

    Task<TReturn> HandleUx(Socket socket, T unit);


}
