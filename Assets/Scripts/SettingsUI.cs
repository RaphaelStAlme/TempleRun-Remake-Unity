using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI instance;
    
    [SerializeField] GameObject panel;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPseudo(string playerName)
    {
        if (playerName != null)
        {
            PlayerPrefs.SetString("playerName", playerName);
        }
    }

    public string GetPseudo()
    {
        return PlayerPrefs.GetString("playerName", "Anonymous");
    }

    public void ShowPanel()
    {
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
