

using System.Formats.Tar;
using System.Net;
using Microsoft.AspNetCore.Authentication;

namespace library.worldcomputer.info;

public class InventoryIntentAction : IIntentAction
{
    private IGrid _grid;
    private IImageHandler _imageHandler;

    public string Intent => "inventory";

    public InventoryIntentAction(IGrid grid, IImageHandler imageHandler)
    {
        _grid = grid;
        _imageHandler = imageHandler;
    }

    public async Task<IntentActionResult> Exec(string intentPath, IUnit unit, Socket socket)
    {
        var iar = new IntentActionResult();
        iar.Success = false;
        if (unit is Body)
        {
            var b = unit as Body;

            foreach (var key in b.Suit.Wearables.Keys)
            {
                await $"{key.ToString().Emph()} : {b.Suit.Wearables[key].Name.Info()}".Send(socket);
            }

            iar.Success = true;
        }
        return iar;
    }
}