namespace library.worldcomputer.info;

public class BodySuit
{

    public Dictionary<UnitPart, Item> Wearables {get;set;}

    private IUnit _unit;

    public BodySuit()
    {
        Wearables = new Dictionary<UnitPart, Item>();
    
        foreach (UnitPart part in Enum.GetValues(typeof(UnitPart)))
        {
            Wearables[part] = Item.Empty;
        }
        
    }

    public BodySuit(IUnit unit)
    {
        Wearables = new Dictionary<UnitPart, Item>();
    
        foreach (UnitPart part in Enum.GetValues(typeof(UnitPart)))
        {
            Wearables[part] = Item.Empty;
        }
        
        _unit = unit;
 
    }
    
    public void New()
    {
        Wearables[UnitPart.Back] = new TechBackpack();
        Wearables[UnitPart.LeftHand] = new HighTech_Lunchbox();
        Wearables[UnitPart.LeftArm] = new Calculator_Watch();
    }
}