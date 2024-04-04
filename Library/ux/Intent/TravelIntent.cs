
using System.Text.RegularExpressions;

namespace library.worldcomputer.info;

public class TravelIntent : IIntent
{

    string[] _directions = new[] {
        "up",
        "down",
        "north",
        "south",
        "east",
        "west"
    };

    string _numberRegexPattern = "(/d*)";
    Regex _numberRegex;
    private IWordResolver _wordResolver;

    public TravelIntent(IWordResolver wordResolver)
    {
        _numberRegex = new Regex(_numberRegexPattern);
        _wordResolver = wordResolver;
    }

    public async Task<TryParseResult> TryParse(string input)
    {
        // resolve words like travel to travel, move, go, ect.
        var result = await _wordResolver.Resolve(input, PartOfSpeech.verb, "travel");

        // if we resolve travel, resolve the direction
        if (result == "travel")
        {
            var tpr = new TryParseResult
            {
                IntentPath = "travel.",
            };
            
            result = await _wordResolver.Resolve(input, PartOfSpeech.adv, _directions);

            if (_directions.Any(x=> x == result))
            {
                tpr.IntentPath += result;
                tpr.Intent = "travel";
                tpr.Success = true;

                var match = _numberRegex.Match(input);
                
                if (match.Captures.Any())
                {
                    tpr.IntentPath += "." + match.Captures.First().Value;
                }

                return tpr;                
            }
        }

        return new TryParseResult { Success = false};


    }
}
