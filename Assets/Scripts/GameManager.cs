using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int restEsquive;
    public static bool playerIsDied;
    public static string score;

    [SerializeField] private GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        restEsquive = 1;
        playerIsDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsDied)
        {
            Time.timeScale = 0;
            gameOverUI.SetActive(true);
        }
    }


}
