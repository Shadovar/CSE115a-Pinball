using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    public static int scoreValue = 0;
    public static int currentHighScore = 0;
    public static List<Tuple<string, int>> highScores;
    public const string defaultScorePath = "score.txt";
    Text score;

    /// <summary>
    /// Updates the hogh score whenever scorevalue is updated.
    /// </summary>
    public static int scorevalue
    {
        get { return scoreValue; }
        set
        {
            currentHighScore = Math.Max(currentHighScore, value);
            scoreValue = value;
        }
    }

    /// <summary>
    /// Gets the tuple defining the global high score.
    /// </summary>
    /// <returns>Tuple pair containing (user name, high score).</returns>
    public static Tuple<string, int> GetGlobalHigh()
    {
        int bestIndex = -1;
        int bestScore = -1;
        for (int i = 0; i < highScores.Count; i++)
        {
            if (highScores[i].Item2 > bestScore)
            {
                bestIndex = i;
                bestScore = highScores[i].Item2;
            }
        }

        if (bestIndex >= 0)
        {
            return highScores[bestIndex];
        }
        else
        {
            return new Tuple<string, int>("Empty", 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        highScores = new List<Tuple<string, int>>();
    }

    // Update is called once per frame
    void Update()
    {
        Tuple<string, int> globalHigh = GetGlobalHigh();
        score.text = "score: " + scorevalue + " (Personal High " + currentHighScore + ")\nGlobal High: " +
            globalHigh.Item1 + " " + globalHigh.Item2;
    }

    /// <summary>
    /// Adds the current high score to a slot in the score table.
    /// </summary>
    /// <param name="name">The name of the player who got current high score.</param>
    public static void CommitScore(string name = "UnNamed")
    {
        if (name.Contains(" ") || name.Contains("\n") || name.Contains("\t"))
        {
            /// Name cannot have white space or new-lines
            Debug.Log("Invalid name passed to CommitScore.");
        }
        else
        {
            bool nameExists = false;
            for (int i = 0; i < highScores.Count; i++)
            {
                if (highScores[i].Item1 == name)
                {
                    // Name already exists, update it with higher score value
                    highScores[i] = new Tuple<string, int>(name, Math.Max(highScores[i].Item2, currentHighScore));
                    nameExists = true;
                    break;
                }
            }
            if (!nameExists)
            {
                // Create a new high score entry
                highScores.Add(new Tuple<string, int>(name, currentHighScore));
            }
        }
    }

    /// <summary>
    /// Loads the score file.
    /// </summary>
    /// <param name="path">Path to score file.</param>
    /// <remarks>If the file cannot be opened and read, the default score of 0 is used.</remarks>
    public static void Load(string path = defaultScorePath)
    {
        highScores = new List<Tuple<string, int>>();
        try
        {
            string[] scores = File.ReadAllText(path).Split('\n');
            for (int i = 0; i < scores.Length; i++)
            {
                string thisName = scores[i].Split(' ')[0];
                string thisScoreString = scores[i].Split(' ')[1];
                int thisScoreValue;
                if (!int.TryParse(thisScoreString, out thisScoreValue))
                {
                    Debug.Log("Failed to parse high score for " + thisName + ", defaulting to 0.");
                    thisScoreValue = 0;
                }
                highScores.Add(new Tuple<string, int>(thisName, thisScoreValue));
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to load score file.");
        }
    }

    /// <summary>
    /// Saves the score data to a file.
    /// </summary>
    /// <param name="path">Path to the save file.</param>
    /// <remarks>Overwrites any already existing file in path if one exists.</remarks>
    public static void Save(string path = defaultScorePath)
    {
        try
        {
            string scoreString = "";
            for (int i = 0; i < highScores.Count; i++)
            {
                scoreString += highScores[i].Item1 + " " + highScores[i].Item2 + "\n";
            }
            File.WriteAllText(path, scoreString);
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to save score file.");
            throw;
        }
    }
}
