using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;

namespace library.worldcomputer.info;


public class TechBackpack : Item
{
    public override string Name => "Tech_Backpack";

    public TechBackpack(IImageHandler imageHandler):base(imageHandler)
    {

        EnergySlotsMax.Values.ForAll(x => x = 1);
        EnergySlots.Values.ForAll(x => x = 1);

        ItemSlots =
        [
            Item.Empty,
            Item.Empty
        ];
    }
}


