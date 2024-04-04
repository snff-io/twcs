using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;



public class WordNet : IWordResolver
{

    static Regex dataRx;
    static string dataRxPattern = @"[a-z_]*\s[0-9]\s";
    const string BASE_PATH = "/srv/wordnet/dict/";

    static WordNet()
    {
        dataRx = new Regex(dataRxPattern);
    }


    public async Task<string> Resolve(string input, PartOfSpeech pos, params string[] cmd_words)
    {
        using (var fidx = File.OpenRead(($"{BASE_PATH}/index.{pos}")))
        using (var reader = new StreamReader(fidx))
        {
            input = input.ToLower();
            var qwords = input.Split(' ');

            // Check if any of the command words are present in the input
            var matchingCmdWord = qwords.FirstOrDefault(qword => cmd_words.Contains(qword));
            if (matchingCmdWord != null)
            {
                return matchingCmdWord;
            }

            // Process each line in the index file
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (line == null)
                    continue;

                line = line.ToLower();
                var words = input.ToLower().Split(' ');

                // Check if the line starts with any word from the input
                var matchingWord = words.FirstOrDefault(word => line.StartsWith(word));
                if (matchingWord == null)
                    continue;

                // Regular expression pattern to match 8 consecutive digits
                string pattern = @"\b\d{8}\b";

                // Find all matches of 8-digit groups in the line
                var idxmatches = Regex.Matches(line, pattern);

                // Process each match in the line
                foreach (Match idxmatch in idxmatches)
                {
                    using (var fdata = File.OpenRead($"{BASE_PATH}/data.{pos}"))
                    using (var freader = new StreamReader(fdata))
                    {
                        fdata.Seek(int.Parse(idxmatch.Value), SeekOrigin.Begin);
                        var fline = freader.ReadLine();

                        if (fline == null)
                            continue;

                        // Check if any of the command words are present in the line
                        foreach (var fword in cmd_words)
                        {
                            var matches = dataRx.Matches(fline);

                            foreach (Match match in matches)
                            {
                                // Loop over captures
                                foreach (Capture capture in match.Captures)
                                {
                                    // Check if the captured word matches a specific word
                                    if (capture.Value.ToLower() == fword.ToLower())
                                    {
                                        return fword;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return "";
    }


    // public async Task<string> Resolve(string input, PartOfSpeech pos, params string[] cmd_words)
    // {

    //     using (var fidx = File.OpenRead(($"{BASE_PATH}/index.{pos}")))
    //     using (var reader = new StreamReader(fidx))
    //     {
    //         var qwords = input.Split(' ');

    //         foreach (var qword in qwords)
    //         {
    //             if (cmd_words.Contains(qword))
    //                 return qword;
    //         }

    //         while (!reader.EndOfStream)
    //         {
    //             var line = await reader.ReadLineAsync();
    //             line = line?.ToLower();
    //             var words = input.ToLower().Split(' ');
    //             foreach (var word in words)
    //             {
    //                 if (line == null)
    //                     break;

    //                 if (!line.StartsWith(word))
    //                     continue;

    //                 // Regular expression pattern to match 8 consecutive digits
    //                 string pattern = @"\b\d{8}\b";

    //                 // Find all matches of 8-digit groups in the line
    //                 var idxmatches = Regex.Matches(line, pattern);
    //                 // Iterate over matches and add them to the synset offsets

    //                 using (var fdata = File.OpenRead($"{BASE_PATH}/data.{pos}"))
    //                 using (var freader = new StreamReader(fdata))
    //                 {
    //                     foreach (Match idxmatch in idxmatches)
    //                     {

    //                         fdata.Seek(int.Parse(idxmatch.Value), SeekOrigin.Begin);
    //                         var fline = freader.ReadLine();

    //                         if (fline == null)
    //                             continue;

    //                         foreach (var fword in cmd_words)
    //                         {
    //                             var matches = dataRx.Matches(fline);

    //                             foreach (Match match in matches)
    //                             {
    //                                 // Loop over captures
    //                                 foreach (Capture capture in match.Captures)
    //                                 {
    //                                     // Check if the captured word matches a specific word
    //                                     if (capture.Value.ToLower() == fword.ToLower())
    //                                     {
    //                                         return capture.Value;
    //                                     }
    //                                 }
    //                             }
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //     }

    //     return "";
    // }

    // open "index.verb", scan foreach word in input, gather indexes, seek in data.verb at indexes looking for a preferred verb  
}


public enum PartOfSpeech
{
    noun,
    verb,
    adj,
    adv
}

// string[] cmd_verbs = {
//     "travel",
//     "use",
//     "take",
//     "put",
//     "examine",
//     "speak",
//     "laugh"
// };

// string[] cmd_nouns =
// {
//     "device",
//     "location",
//     "energy",
//     "pair",
//     "vibration"
// };