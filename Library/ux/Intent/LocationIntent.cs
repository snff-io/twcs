
using System.Text.RegularExpressions;

namespace library.worldcomputer.info;

public class LocationIntent : IIntent
{

    string[] _operations = new[] {
        "buy",
        "sell",
        "check",
        "withdraw",
    };

    string _numberRegexPattern = "(/d*)";
    Regex _numberRegex;
    private IWordResolver _wordResolver;

    public LocationIntent(IWordResolver wordResolver)
    {
        _numberRegex = new Regex(_numberRegexPattern);
        _wordResolver = wordResolver;
    }

    public async Task<TryParseResult> TryParse(string input)
    {
        var tpr = new TryParseResult();
        // resolve words like travel to travel, move, go, ect.
        var result = await _wordResolver.Resolve(input, PartOfSpeech.noun, "location");

        if (result == "location")
        {
            tpr.Success = true;
            tpr.Intent = "location";
            tpr.IntentPath = "location";
            return tpr;
        }

        tpr.Success = false;
        return  tpr;
    }
}