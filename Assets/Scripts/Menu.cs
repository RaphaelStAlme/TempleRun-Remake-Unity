using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject levelSelectorPanel;
    public GameObject settingsPanel;
    public GameObject scorePanel;

    public void SetLevelSelectorPanel()
    {
        levelSelectorPanel.SetActive(true);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
