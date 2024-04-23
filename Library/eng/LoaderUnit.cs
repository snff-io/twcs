using System.Reflection.Metadata.Ecma335;

namespace library.worldcomputer.info;
class LoaderUnit : IUnit
{
    public string DisplayType
    {
        get
        {
            return this.displayTypeString;
        }
    }

    public string GetHash(int length)
    {
        throw new NotImplementedException();
    }

    public string _id
    {
        get
        {
            return (FirstName + "_" + LastName).Replace(" ", "-");
        }
    }
    string displayTypeString = "";
public Location Location {get;set;}
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    string IUnit.Id { get; set; }
    
    bool IUnit.Bound {get;set;}
    public string Secret { get;set; }
    public DateTime LastLogin { get;set; }
    public Dictionary<Awareness, int> Awareness { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public LoaderUnit(string displayTypeString)
    {
        this.displayTypeString = displayTypeString;

    }

}