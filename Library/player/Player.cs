using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace library.worldcomputer.info;

public class Player : IPlayer
{
    IMove _mover;
    private IStatus _status;
    private IInformer _informer;

    public ILocation Location { get; set; }

    public string Name
    {
        get; set;
    }

    public Player(IMove mover, IStatus status, IInformer informer)
    {
        _mover = mover;
        _status = status;
        _informer = informer;
    }

    public bool Move(Direction direction)
    {
        var moved = _mover.TryMove(Location, direction);
        if (!moved) {
            _informer.Inform(_status[0]);
            return false;
        }

        return true;
    }

}