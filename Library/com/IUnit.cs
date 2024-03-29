using Amazon.DynamoDBv2.Model;

namespace library.worldcomputer.info;
public interface IUnit : IHash
{
    string Id { get; set; }

    string DisplayType {get;}

    static string ToId(string value)
    {
        return value.Trim().Replace(" ", "_");
    }
    
}