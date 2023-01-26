using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int restEsquive;
    public static bool playerIsDied;
    public static int score;

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
            Debug.Log("YES");
            Time.timeScale = 0;
            InputSystem.DisableDevice(Keyboard.current);
            gameOverUI.SetActive(true);
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        restEsquive = 1;
        playerIsDied = false;
        InputSystem.EnableDevice(Keyboard.current);
    }

    public void Retry()
    {
        gameOverUI.SetActive(false);
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        ///Revoir pour la réinitialisation de  la vie pour l'appliquer dans le script du joueur
        gameOverUI.SetActive(false);
        Resume();
        SceneManager.LoadScene("Menu");
    }


}
