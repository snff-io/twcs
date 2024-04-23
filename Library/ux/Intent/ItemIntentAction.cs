

using System.ComponentModel.Design;
using System.Formats.Tar;
using System.Net;
using Microsoft.AspNetCore.Authentication;

namespace library.worldcomputer.info;

public class ItemIntentAction : IIntentAction
{
    private IGrid _grid;
    private IImageHandler _imageHandler;

    public string Intent => "item";

    public ItemIntentAction(IGrid grid, IImageHandler imageHandler)
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
            var cnt = 0;
            foreach (var key in b.Suit.Wearables.Keys)
            {
                if (b.Suit.Wearables[key] != Item.Empty)
                {
                    cnt++;
                    await $"{cnt}) {key.ToString().Emph()} : {b.Suit.Wearables[key].Name.Info()}".Send(socket);
                }
            }

            iar.Success = true;
        }
        return iar;
    }
}