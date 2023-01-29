using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI instance;
    
    [SerializeField] GameObject panel;
    [SerializeField] TMP_InputField playerNameField;

    private void Awake()
    {
        instance = this;
    }

    public void SetPseudo(string playerName)
    {
        if (playerName != null)
        {
            PlayerPrefs.SetString("playerName", playerName);
        }
    }

    public void GetPseudoInputText()
    {
        Debug.Log("CALLED");
        playerNameField.text = GetPseudo();
    }

    public string GetPseudo()
    {
        return PlayerPrefs.GetString("playerName", "Anonymous");
    }

    public void ResetScores()
    {
        HighScoreManager.instance.ResetScores();
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        GetPseudoInputText();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
