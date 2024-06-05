using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Analyzer der W�rter z�hlt die �fter vorkommen (Logik mithilfe KI ertsellt)
/// </summary>
public static class TextAnalyzer
{
    public static List<KeyValuePair<string, int>> FindMostFrequentWords(string content, int topN)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return new List<KeyValuePair<string, int>>();
        }

        string[] words = content.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

        Dictionary<string, int> wordCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (string word in words)
        {
            if (wordCount.ContainsKey(word))
            {
                wordCount[word]++;
            }
            else
            {
                wordCount[word] = 1;
            }
        }

        return wordCount.OrderByDescending(w => w.Value).Take(topN).ToList();
    }
}
