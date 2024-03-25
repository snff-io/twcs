public interface IWordResolver 
{
    public string Resolve(string input, PartOfSpeech pos, params string[] cmd_words);

}