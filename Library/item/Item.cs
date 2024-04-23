
namespace library.worldcomputer.info;

public class Item
{

    static Item _empty;
    public static Item Empty
    {
        get
        {
            if (Item._empty == null)
            {
                _empty = new EmptyItem();
            }
            return _empty;
        }
    }

    public virtual Guid Instance { get; set; }

    public virtual int Quality { get; set; }

    public virtual string Name { get; set; }

    public Item()
    {

    }

    public Item[] ItemSlots { get; protected set; }
    public Dictionary<Domain, double> EnergySlotsMax { get; protected set; } = new Dictionary<Domain, double>()
    {
        {Domain.Earth, 0},
        {Domain.Fire, 0},
        {Domain.Gorge, 0},
        {Domain.Heaven, 0},
        {Domain.Mountain, 0},
        {Domain.Swamp, 0},
        {Domain.Thunder, 0},
        {Domain.Wind, 0},
    };

    public Dictionary<Domain, double> EnergySlots { get; private set; } = new Dictionary<Domain, double>()
    {
        {Domain.Earth, 0},
        {Domain.Fire, 0},
        {Domain.Gorge, 0},
        {Domain.Heaven, 0},
        {Domain.Mountain, 0},
        {Domain.Swamp, 0},
        {Domain.Thunder, 0},
        {Domain.Wind, 0},
    };


    public Dictionary<Awareness, double> PassiveEffects { get; private set; } = new Dictionary<Awareness, double>()
    {
        {Awareness.Automation, 0},
        {Awareness.Communication, 0},
        {Awareness.Connection, 0},
        {Awareness.Energy, 0},
        {Awareness.Environment, 0},
        {Awareness.Navigation, 0},
        {Awareness.Perception, 0},
        {Awareness.Vibration, 0},
    };

    public Dictionary<Domain, double> EnergyRequirement { get; private set; } = new Dictionary<Domain, double>()
    {
        {Domain.Earth, 0},
        {Domain.Fire, 0},
        {Domain.Gorge, 0},
        {Domain.Heaven, 0},
        {Domain.Mountain, 0},
        {Domain.Swamp, 0},
        {Domain.Thunder, 0},
        {Domain.Wind, 0},
    };

    public void Load(Domain type, double magnitude)
    {
        //TODO Calculate and fuzz loss
        EnergySlots[type] += Math.Max(EnergySlotsMax[type], EnergySlots[type] + magnitude );
    }

    public void Load(Pair source, double magnitude)
    {
        //TODO Calculate and fuzz loss
        Load(source.BottomTrigram.Domain, magnitude );
        Load(source.TopTrigram.Domain, magnitude );
        
        source.Magnitude -= Math.Min(0, source.Magnitude - magnitude);
    }
}

public class EmptyItem : Item
{
    public EmptyItem()
    {
    }
}