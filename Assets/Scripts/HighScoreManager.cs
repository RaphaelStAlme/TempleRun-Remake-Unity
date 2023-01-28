using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager instance;

    public HighScoreElements highScores = new HighScoreElements();
    private String saveFile;

    private void Awake()
    {
        instance = this;
        saveFile = $"{Application.persistentDataPath}/highscores.json";
    }

    private void Start()
    {
        LoadHighScores();
    }

    // Start is called before the first frame update

    public void LoadHighScores()
    {
        if(File.Exists(saveFile)) {
            Debug.Log(saveFile);
            string json = File.ReadAllText(saveFile);
            highScores = JsonUtility.FromJson<HighScoreElements>(json); 
        }
    }

    public void SaveHighScore(HighScoreElement highScore)
    {
        highScores.highScoreElements.Add(highScore);
        var jsonList = JsonUtility.ToJson(highScores);
        File.WriteAllText(saveFile, jsonList);
    }
}

[Serializable]
public class HighScoreElements
{
    public List<HighScoreElement> highScoreElements;
}

[Serializable]
public class HighScoreElement
{
    public string playerName;
    public string score;
}
