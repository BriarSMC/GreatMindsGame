using UnityEngine;
using System.Reflection;
using System;

/**
 *
 * Copyright © 2025 by Steven M. Coghill
 * This project is licensed under the MIT License.
 * A copy of the MIT License can be found in the 
 * accompanying LICENSE.txt file.
 **/

/** 
 * https://games.coghillclan.net/GreatMinds
 * 
 * https://www.github.com/BriarSMC/GreatMindsGame.git
 *
 * Version: 0.1.0
 * Version History
 * ----------------------------------------------------------------------------
 * 0.1.0    27-Oct-2025 Using the GreatMinds-Data-Test CS file as a base
 **/

public class WordListManager : MonoBehaviour
{
    /**
     * The word list is in a CSV file located it in the Unity Resources folder (/Assets/Resources/Text/).
     * Each line in this file has one (1) word and one (1) indicator separated by a comma (,). 
     * The indicator can be one of three values: 
     *   - a (after the word)
     *   - b (before the word)
     *   - e (either before or after)
     *
     * The word list file is bundled with the game as Resource. So it must be formatted and
     * validated before the game is built.
     * 
     * At start up we read the Text Asset in string array. We then randomize the array so the
     * game can request words from it in a random order.
     * 
     **/

    /*
     * wordList         - Resource CSV file containing the word list
     * k_WordListName   - Name of the word list in the Assets folder
     * wordEntries      - String array containing each word and its indicator
     * nextWordNdx      - A pointer into the randomized array for fetching entries
     
     */
    TextAsset wordList;

    const string k_WordListName = "wordlist";
    string[] wordEntries;
    int nextWordNdx = 0;


    void Start()
    {
        // Read the word list file into array and call the randomize method.

        wordList = Resources.Load($"Text/{k_WordListName}") as TextAsset;

        if (wordList == null)
        {
            throw new ApplicationException("Could not find the game's word list.");
        }

        wordEntries = wordList.text.Split("\n");
        RandomizeWords();
    }


    void RandomizeWords()
    {
        // Randomize the words.
        // A simple Knuth shuffle algorithm.

        System.Random random = new System.Random();

        int n = wordEntries.Length;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            string value = wordEntries[k];
            wordEntries[k] = wordEntries[n];
            wordEntries[n] = value;
        }
    }


    public string GetNextWord()
    {
        // Return the next word in the randomized array.
        // Wrap around to the beginning if necessary.

        if (nextWordNdx >= wordEntries.Length) nextWordNdx = 0;
        return wordEntries[nextWordNdx++];
    }
}
