

using System.Formats.Tar;
using System.Net;

namespace library.worldcomputer.info;

public class LocationIntentAction : IIntentAction
{
    private IGrid _grid;
    private IImageHandler _imageHandler;

    public string Intent => "location";

    public LocationIntentAction(IGrid grid, IImageHandler imageHandler)
    {
        _grid = grid;
        _imageHandler = imageHandler;
    }

    public async Task<IntentActionResult> Exec(string intentPath, IUnit unit, Socket socket)
    {

        
        var pg = _grid.GetPairGroup(unit.Location.Layer, unit.Location.X, unit.Location.Y);

        var here = pg.Current;

        var image = await _imageHandler.GetRandomLandscape(here.TopTrigram.Domain, here.BottomTrigram.Domain );
        
        await image.Send(socket);
        
        var stability = here.Stability == 1 ? "stable" : "unstable";

        var dlines = here.Description.Split(",");

        foreach (var l in dlines) 
        {
            await l.Text().Send(socket);
        }

        await $"\nThis location seems {stability}".Info().Send(socket);

        return new IntentActionResult() { Success = true, Next = "" };

    }
}


