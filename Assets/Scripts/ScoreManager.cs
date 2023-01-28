using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshPro highScoreTxt;

    float score = 0;
    int highScore = 0;
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
            if(GameManager.playerIsDied || GameManager.playerReachedFinishLine)
            {
                if (PlayerPrefs.HasKey("hiScore"))
{
                    if (score > PlayerPrefs.GetInt("hiScore"))
                    {
                        highScore = (int) score;
                        PlayerPrefs.SetInt("hiScore", highScore);
                        PlayerPrefs.Save();
                    }
                }
                else
                {
                    if (score > highScore)
                    {
                        highScore = (int) score;
                        PlayerPrefs.SetInt("hiScore", highScore);
                        PlayerPrefs.Save();
                    }
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
