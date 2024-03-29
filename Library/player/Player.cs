using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace library.worldcomputer.info;

public class Player : IPlayer
{
    public string DisplayType => "Player";

    public string Id { get; set; }

    public string Chosen { get; set; }

    public DateTime LoggedIn { get; set; }

    public Player()
    {
        
    }

    public string GetHash(int length)
    {
        throw new NotImplementedException();
    }
}