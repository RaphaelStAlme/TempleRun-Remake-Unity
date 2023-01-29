using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI instance;

    [SerializeField] GameObject panel;
    [SerializeField] TMP_InputField playerNameField;
    [SerializeField] InputActionAsset inputActionAsset;
    [SerializeField] TMP_Dropdown dropdown;

    List<InputControlScheme> inputControlSchemes = new List<InputControlScheme>();

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

    public void GetCurrentControlSchemeDropdown()
    {
        dropdown.value = inputControlSchemes[0].name == GetCurrentControlScheme() ? 0 : 1;
    }

    public void GetCurrentPseudoInputText()
    {
        playerNameField.text = GetPseudo();
    }

    public string GetPseudo()
    {
        return PlayerPrefs.GetString("playerName", "Anonymous");
    }

    public void GetControlSchemes()
    {
        inputControlSchemes = inputActionAsset.controlSchemes.ToList();
        foreach (var controlScheme in inputControlSchemes)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(controlScheme.name));
        }
    }

    public string GetCurrentControlScheme()
    {
        return PlayerPrefs.GetString("currentControlScheme");
    }

    public void SetControlScheme(int indexControlScheme)
    {
        PlayerPrefs.SetString("currentControlScheme", inputControlSchemes[indexControlScheme].name);
    }

    public void ResetScores()
    {
        HighScoreRegister.instance.ResetScores();
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
        GetCurrentPseudoInputText();
        GetCurrentControlSchemeDropdown();
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
