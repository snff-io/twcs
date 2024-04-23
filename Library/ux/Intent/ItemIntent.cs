
using System.Text.RegularExpressions;

namespace library.worldcomputer.info;

public class ItemIntent : IIntent
{

    string[] _operations = new[] {
        "use",
        "load",
        "drop",
        "examine"
    };

    string _numberRegexPattern = "(/d*)";
    Regex _numberRegex;
    private IWordResolver _wordResolver;

    public ItemIntent(IWordResolver wordResolver)
    {
        _numberRegex = new Regex(_numberRegexPattern);
        _wordResolver = wordResolver;
    }

    public async Task<TryParseResult> TryParse(string input)
    {
        var tpr = new TryParseResult();
        // resolve words like travel to travel, move, go, ect.
        var result = await _wordResolver.Resolve(input, PartOfSpeech.noun, "use");

        if (result == "use")
        {
            tpr.Success = true;
            tpr.Intent = "use";
            tpr.IntentPath = "use";
            return tpr;
        }

        tpr.Success = false;
        return  tpr;
    }
}