namespace library.worldcomputer.info;

public class Location: ILocation {
    public int Layer {get;set;} = 1;
    public int X {get;set;} = 0;
    public int Y {get;set;} = 0;

    public Location Fuzz(double error)
    {
        error = error * .01;

        var location = new Location()
        {
            //TODO Rand +/-
            X = (int)Math.Ceiling(this.X + error),
            Y = (int)Math.Floor(this.X + error),
            Layer = this.Layer

        };

        return location;
    }

    public override string ToString()
    {
        return $"{Layer}:{X},{Y}";
    }
}