using System;
using System.Collections.Generic;
using System.IO;

public class WordNet
{
    private Dictionary<string, List<string>> wordToSynsets;
    private Dictionary<string, List<string>> synsetToWords;

    public WordNet(string wordNetDirectory)
    {
        wordToSynsets = new Dictionary<string, List<string>>();
        synsetToWords = new Dictionary<string, List<string>>();
        LoadWordNet(wordNetDirectory);
    }

    public List<string> GetSynsets(string word)
    {
        if (wordToSynsets.ContainsKey(word))
        {
            return wordToSynsets[word];
        }
        return new List<string>(); // Return empty list if word not found
    }

    public List<string> GetWordsInSynset(string synset)
    {
        if (synsetToWords.ContainsKey(synset))
        {
            return synsetToWords[synset];
        }
        return new List<string>(); // Return empty list if synset not found
    }

    private void LoadWordNet(string wordNetDirectory)
    {
        // Load data files and construct mappings
        LoadWordToSynsets(Path.Combine(wordNetDirectory, "dict", "data.noun"));
        // Add other data files as needed (e.g., data.verb, data.adj, etc.)
    }

    private void LoadWordToSynsets(string filePath)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(' ');
                string synset = parts[0];
                string[] words = parts[1].Split(',');
                foreach (string word in words)
                {
                    if (!wordToSynsets.ContainsKey(word))
                    {
                        wordToSynsets[word] = new List<string>();
                    }
                    wordToSynsets[word].Add(synset);

                    if (!synsetToWords.ContainsKey(synset))
                    {
                        synsetToWords[synset] = new List<string>();
                    }
                    synsetToWords[synset].Add(word);
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Initialize WordNet with the directory containing WordNet dictionary files
        WordNet wordNet = new WordNet("/home/zampinojosh/src/twcs/Terminal/wn/");

        // Example usage: Get synsets for a word
        string word = "dog";
        List<string> synsets = wordNet.GetSynsets(word);
        Console.WriteLine($"Synsets for '{word}': {string.Join(", ", synsets)}");

        // Example usage: Get words in a synset
        string synset = "dog.n.01";
        List<string> wordsInSynset = wordNet.GetWordsInSynset(synset);
        Console.WriteLine($"Words in synset '{synset}': {string.Join(", ", wordsInSynset)}");
    }
}
