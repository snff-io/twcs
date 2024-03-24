public interface IIntent
{
    public bool TryParse(string arguments, out string intentPath);

}

public interface IIntent<T1, T2>:IIntent
{
    public bool Try(T1 p1, T2 p2);
    
}