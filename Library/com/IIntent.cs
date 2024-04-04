public interface IIntent
{
    public Task<TryParseResult> TryParse(string input);

}

