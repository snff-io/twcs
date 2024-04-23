public class MenuOption
{
    public int Ordinal {get;set;}
    public string Display {get;set;}

    public override string ToString()
    {
        return $"{Ordinal}) {Display}";
    }

}

