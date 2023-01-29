using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI instance;
    
    [SerializeField] GameObject panel;
    [SerializeField] TMP_InputField playerNameField;
    [SerializeField] InputActionAsset inputActionAsset;

    private List<InputControlScheme> controlSchemes;

    private void Awake()
    {
        instance = this;
        GetControlSchemes();
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
        playerNameField.text = GetPseudo();
    }

    public string GetPseudo()
    {
        return PlayerPrefs.GetString("playerName", "Anonymous");
    }

    public void GetControlSchemes()
    {
        controlSchemes = inputActionAsset.controlSchemes.ToList();
        foreach(var controlScheme in controlSchemes)
        {
            Debug.Log(controlScheme.name);
        }
    }

    public string GetCurrentControlScheme()
    {
        return PlayerPrefs.GetString("currentControlScheme");
    }

    public void SetControlScheme(InputControlScheme controlScheme)
    {
        PlayerPrefs.SetString("currentControlScheme", controlScheme.name);
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
