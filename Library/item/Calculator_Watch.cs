namespace library.worldcomputer.info;

public class Calculator_Watch: Item
{

    public override string Name => "Calculator_Watch";

    public Calculator_Watch(IImageHandler imageHandler):base(imageHandler)
    {
        EnergySlots[Domain.Earth] = 5;
        EnergySlots[Domain.Wind] = 5;
        EnergySlotsMax[Domain.Earth] = 5;
        EnergySlotsMax[Domain.Wind] = 5;
        EnergyRequirement[Domain.Earth] = 1;
        EnergyRequirement[Domain.Wind] = 1;
        PassiveEffects[Awareness.Navigation] = 1;
    }

    public Dictionary<string, string> Use(IUnit unit)
    {

        var result = new Dictionary<string,string>();

        if (EnergyRequirement.Values.All(x=> x > 0))
        {
            foreach (var k in EnergyRequirement.Keys)
            {
                //TODO: Check Energy Slots
            }

            result["status"] =  "ok";
            result["description"] = "Your wristwatch heats up and beeps a couple times, I think it worked...";
        }
        else
        {
            result["status"] =  "not_ok";
            result["description"] = "Your wristwatch is out of energy, it does nothing.";
        }

        var error = 10 - Math.Min(0, Quality + unit.Awareness[Awareness.Navigation]);

        var loca = unit.Location.Fuzz(error);

        result["location"] = $"{loca.ToString()}";
        return result;

    }
}