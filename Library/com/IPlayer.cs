using Microsoft.AspNetCore.Mvc.Razor;

namespace library.worldcomputer.info;
public interface IPlayer : IUnit
{
    public List<string> Chosen { get; set; }
}
