using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace library.worldcomputer.info;

public class Player : IPlayer
{
    public string DisplayType => "Player";
 
    public string Id { get;set; }

    public List<string> Chosen {get;set;}

    public Player()
    {
        Chosen = new List<string>();
    }

    public string GetHash(int length)
    {
        throw new NotImplementedException();
    }
}