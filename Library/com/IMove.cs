namespace library.worldcomputer.info;
public interface IMove {
    bool TryMove(ILocation location, Direction direction);
}