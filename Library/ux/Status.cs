public class Status : IStatus
{
    string[] _statuses;
    Dictionary<string, string> _special;

    public Status()
    {
        _statuses = new[] {
            "You can't move in that direction.",
            "You didn't specifiy a direction."


        };

        _special = new Dictionary<string, string>
        {
            {"player_prompt", ">> "}

        };
    }

    public string this[int id]
    {
        get
        {
            return this.Get(id);
        }
    }

    public string this[string id]
    {
        get
        {
            return _special[id];
        }
    }

    public string Get(int id)
    {
        return _statuses[id];

    }



}