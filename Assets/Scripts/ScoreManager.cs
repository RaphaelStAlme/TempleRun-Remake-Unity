using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public bool highScoreSaved = false;

    [SerializeField] private TextMeshProUGUI scoreTxt;
    //[SerializeField] private TextMeshPro highScoreTxt;

    float score = 0;
    int pointIncreasedPerSecond = 10;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreTxt.text = score.ToString();
        //highScoreTxt.text = highScore.ToString();
    }

    private void Update()
    {
        if(Time.timeScale != 0)
        {
            AddPoints(pointIncreasedPerSecond * Time.fixedDeltaTime);
        } else
        {
            if((GameManager.playerIsDied || GameManager.playerReachedFinishLine) && !highScoreSaved)
            {
                var highScore = HighScoreManager.instance.GetTemporaryHighScore() != null ? HighScoreManager.instance.GetTemporaryHighScore() : 0;
                Debug.Log(highScore);
                if(score > highScore)
                {
                    highScore = (int) score;
                    HighScoreElement highScoreElement = new HighScoreElement
                    {
                        // TODO : Faire une comparaison pour voir si le pseudo est renseign�
                        playerName = "Anonymous",
                        score = (int) highScore,
                        levelSelection = LevelSelection.currentLevel
                       
                    };

                    HighScoreManager.instance.SaveHighScore(highScoreElement);
                    highScoreSaved = true;
                    
                }
            }
        }
    }

    public void AddPoints(float pointsNumber)
    {
        score += pointsNumber;
        scoreTxt.text = ((int)score).ToString();
    }
}