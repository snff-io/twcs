public interface IIntent
{
    public Task<TryParseResult> TryParse(string arguments);

}

public interface IIntent<T1, T2>:IIntent
{
    public bool Try(T1 p1, T2 p2);
    
}