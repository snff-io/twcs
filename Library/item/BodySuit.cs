namespace library.worldcomputer.info;

public class BodySuit
{
    private IImageHandler _imageHandler;

    public Dictionary<UnitPart, Item> Wearables {get;set;}

    private IUnit _unit;

    public BodySuit(IImageHandler imageHandler)
    {
        _imageHandler = imageHandler;
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
        //TODO IOCDI
        Wearables[UnitPart.Back] = new TechBackpack(_imageHandler);
        Wearables[UnitPart.LeftHand] = new HighTech_Lunchbox(_imageHandler);
        Wearables[UnitPart.LeftArm] = new Calculator_Watch(_imageHandler);
    }
}