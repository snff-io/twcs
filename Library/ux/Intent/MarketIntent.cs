
using System.Text.RegularExpressions;

namespace library.worldcomputer.info;

public class MarketIntent : IIntent
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

    public MarketIntent(IWordResolver wordResolver)
    {
        _numberRegex = new Regex(_numberRegexPattern);
        _wordResolver = wordResolver;
    }

    public async Task<TryParseResult> TryParse(string input)
    {
        // resolve words like travel to travel, move, go, ect.
        var result = await _wordResolver.Resolve(input, PartOfSpeech.noun, "market");

        // if we resolve travel, resolve the direction
        if (result == "market")
        {

            var tpr = new TryParseResult
            {
                Intent = "market",
                IntentPath = "market",
                Success = true
                
            };
            
            result = await _wordResolver.Resolve(input, PartOfSpeech.verb, _operations);

            if (_operations.Any(x=> x == result))
            {                
                tpr.IntentPath += "." + result;
                tpr.Success = true;

                             
            }

            return tpr;
        }

        return new TryParseResult { Success = false};


    }
}
