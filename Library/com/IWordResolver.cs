public interface IWordResolver 
{
    public Task<string> Resolve(string input, PartOfSpeech pos, params string[] cmd_words);

}