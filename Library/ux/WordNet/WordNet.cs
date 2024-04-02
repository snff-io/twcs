using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;



public class WordNet : IWordResolver
{
    const string BASE_PATH = "/srv/wordnet/dict/";

    string[] cmd_verbs = {
        "travel",
        "use",
        "take",
        "put",
        "examine",
        "speak",
        "laugh"
    };

    string[] cmd_nouns =
    {
        "device",
        "location",
        "energy",
        "pair",
        "vibration"
    };

    public async Task<string> Resolve(string input, PartOfSpeech pos, params string[] cmd_words)
    {

        using (var fidx = File.OpenRead(($"{BASE_PATH}/index.{pos}")))
        using (var reader = new StreamReader(fidx))
        {
            var qwords = input.Split(' ');

            foreach (var qword in qwords)
            {
                if (cmd_words.Contains(qword))
                    return qword;
            }

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                line = line?.ToLower();
                var words = input.ToLower().Split(' ');
                foreach (var word in words)
                {
                    if (line == null)
                        break;

                    if (!line.StartsWith(word))
                        continue;

                    // Regular expression pattern to match 8 consecutive digits
                    string pattern = @"\b\d{8}\b";

                    // Find all matches of 8-digit groups in the line
                    var matches = Regex.Matches(line, pattern);
                    // Iterate over matches and add them to the synset offsets

                    using (var fverb = File.OpenRead($"{BASE_PATH}/data.{pos}"))
                    using (var vreader = new StreamReader(fverb))
                    {
                        foreach (Match match in matches)
                        {

                            fverb.Seek(int.Parse(match.Value), SeekOrigin.Begin);
                            var fverbLine = vreader.ReadLine();

                            if (fverbLine == null)
                                continue;

                            foreach (var verb in cmd_words)
                            {
                                if (fverbLine.ToLower().Contains(verb))
                                    return verb;
                            }
                        }
                    }
                }
            }
        }

        return "";
    }

    // open "index.verb", scan foreach word in input, gather indexes, seek in data.verb at indexes looking for a preferred verb  
}


public enum PartOfSpeech
{
    noun,
    verb,
    adj,
    adv

}

