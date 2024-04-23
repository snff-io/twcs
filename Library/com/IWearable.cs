namespace library.worldcomputer.info;

public interface IWearable<T>
{
    public string[] Intents {get; set;}

    public UnitPart On {get;set;}
}


