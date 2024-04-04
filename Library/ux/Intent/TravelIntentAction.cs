
namespace library.worldcomputer.info;

public class TravelIntentAction : IIntentAction
{
    private IGrid _grid;
    private IDal<Body> _bodyDal;
    private IDal<Spirit> _spiritDal;

    public string Intent => "travel";

    public TravelIntentAction(IGrid grid, IDal<Body> bodyDal, IDal<Spirit> spiritDal)
    {
        _grid = grid;
        _bodyDal = bodyDal;
        _spiritDal = spiritDal;
    }

    public async Task<IntentActionResult> Exec(string intentPath, IUnit unit, Socket socket)
    {

        var iar = new IntentActionResult();
        iar.Next = "location";

        var args = intentPath.Split('.');

        var direction = args[1];

        if (direction == "up" || direction == "down")
        {
            await "You can't do that... yet".Info().Send(socket);
            iar.Success = false;
            return iar;
        }

        var pg = _grid.GetPairGroup(unit.Location.Layer, unit.Location.X, unit.Location.Y);

        var gotoPair = pg.Get(direction);

        if (gotoPair == Pair.None)
        {
            await "You can't move that direction.".Info().Send(socket);
            iar.Success = false;
            return iar;
        }

        if (unit is Body)
        {
            unit.Location.Layer = gotoPair.Layer;
            unit.Location.Y = gotoPair.Y;
            unit.Location.X = gotoPair.X;
            iar.Success = true;
            await _bodyDal.Put((Body)unit);
            iar.Success = true;
            return iar;
        }
        else if (unit is Spirit)
        {
            await "not yet!".Error().Send(socket);
        }

        iar.Next = "";
        iar.Success = false;
        return iar;
    }
}