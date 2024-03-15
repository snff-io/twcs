namespace library.worldcomputer.info;
class AttributesSpirit : IAttributes
{
    public int energy;
    public int vibration;

    public int Energy { get; set; }
    public int Vibration { get; set; }
    public int Get(string key)
    {
        throw new System.NotImplementedException();
    }

    public int[] Roll(string[] against)
    {
        throw new System.NotImplementedException();
    }
}