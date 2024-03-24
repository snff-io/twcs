using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Terminal.Gui.Graphs;
using System.Text.RegularExpressions;



public class WordNet
{
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

    public string Resolve(string input, PartOfSpeech pos)
    {
        string[] cmd_words;
        switch (pos) {
            case PartOfSpeech.noun: 
                cmd_words = cmd_nouns;
                break;
            case PartOfSpeech.verb:
                cmd_words = cmd_verbs;
                break;
            default:
                throw new Exception("no, bad");

        }       

        using (var fidx = File.OpenRead("/home/zampinojosh/src/twcs/Terminal/wn/dict/index." + pos.ToString()))
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
                var line = reader.ReadLine();
                var words = input.Split(' ');
                foreach (var word in words)
                {

                    if (!line.StartsWith(word))
                        continue;

                    // Regular expression pattern to match 8 consecutive digits
                    string pattern = @"\b\d{8}\b";

                    // Find all matches of 8-digit groups in the line
                    var matches = Regex.Matches(line, pattern);
                    // Iterate over matches and add them to the synset offsets

                    using (var fverb = File.OpenRead("/home/zampinojosh/src/twcs/Terminal/wn/dict/data." + pos.ToString()))
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
class Program
{
    static void Main(string[] args)
    {
        var wn = new WordNet();

        System.Console.WriteLine(wn.Resolve(args[0],PartOfSpeech.verb));
        System.Console.WriteLine(wn.Resolve(args[0],PartOfSpeech.noun));
    }
}
