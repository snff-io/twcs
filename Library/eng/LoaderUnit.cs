using System.Reflection.Metadata.Ecma335;

namespace library.worldcomputer.info;
class LoaderUnit : IUnit
{
    public string DisplayType()
    {
        return this.displayTypeString;
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

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    string IUnit._id { get; set; }

    public LoaderUnit(string displayTypeString)
    {
        this.displayTypeString = displayTypeString;

    }

}