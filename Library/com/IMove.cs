namespace library.worldcomputer.info;
public interface IMove:IIntent
{
    
    bool TryMove(ILocation location, Direction direction);
}