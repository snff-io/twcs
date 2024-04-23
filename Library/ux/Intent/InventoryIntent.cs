
using System.Text.RegularExpressions;

namespace library.worldcomputer.info;

public class InventoryIntent : IIntent
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

    public InventoryIntent(IWordResolver wordResolver)
    {
        _numberRegex = new Regex(_numberRegexPattern);
        _wordResolver = wordResolver;
    }

    public async Task<TryParseResult> TryParse(string input)
    {
        var tpr = new TryParseResult();
        // resolve words like travel to travel, move, go, ect.
        var result = await _wordResolver.Resolve(input, PartOfSpeech.noun, "inventory");

        if (result == "inventory")
        {
            tpr.Success = true;
            tpr.Intent = "inventory";
            tpr.IntentPath = "inventory";
            return tpr;
        }

        tpr.Success = false;
        return  tpr;
    }
}