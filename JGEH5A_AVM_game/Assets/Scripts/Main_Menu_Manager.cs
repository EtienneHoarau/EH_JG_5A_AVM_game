using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    [SerializeField] private Canvas settingsPanel;
  
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadTuto()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void ActivateSettings()
    {
        settingsPanel.gameObject.SetActive(true);
    }
}
