using Amazon.DynamoDBv2.Model;

namespace library.worldcomputer.info;
public interface IUnit : IHash
{
    string Id { get; set; }
    string Secret { get; set; }

    DateTime LastLogin { get; set; }

    string FirstName { get; set; }
    string LastName { get; set; }

    Location Location { get; set; }

    bool Bound { get; set; }

    static string ToId(string value)
    {
        return value.Trim().Replace(" ", "_");
    }

    public string b45Name()
    {
        return FirstName[..2].b91() + LastName[..2]?.b91();

    }


}