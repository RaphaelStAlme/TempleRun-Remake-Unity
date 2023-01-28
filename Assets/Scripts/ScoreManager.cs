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
        }
    }

    public void AddPoints(float pointsNumber)
    {
        score += pointsNumber;
        scoreTxt.text = ((int)score).ToString();
    }
}
