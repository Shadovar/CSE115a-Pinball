using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreControl : MonoBehaviour
{
    public static int scorevalue = 0;
    public const string defaultScorePath = "score.txt";
    Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "score: " + scorevalue;
    }

    public static void Load(string path = defaultScorePath)
    {
        try
        {
            string score = File.ReadAllText(path);
            if (!int.TryParse(score, out scorevalue))
            {
                Debug.Log("Failed to parse score file. Defaulting to 0.");
                scorevalue = 0;
            }
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to load score. Defaulting to 0.");
            scorevalue = 0;
            throw;
        }
    }

    public static void Save(string path = defaultScorePath)
    {
        try
        {
            string score = scorevalue.ToString();
            File.WriteAllText(path, score);
        }
        catch (System.Exception)
        {
            Debug.Log("Failed to save score file.");
            throw;
        }
    }
}
