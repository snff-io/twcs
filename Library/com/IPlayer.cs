using Microsoft.AspNetCore.Mvc.Razor;

namespace library.worldcomputer.info;
public interface IPlayer {

    public string Name {get;set;}

    public ILocation Location {get;set;}

    public bool Move(Direction direction);
}
