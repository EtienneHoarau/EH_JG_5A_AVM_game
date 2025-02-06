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
        SceneManager.LoadScene("Level#1");
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Modified_Map1 Etienne alpha");
    }

    public void ActivateSettings()
    {
        settingsPanel.gameObject.SetActive(true);
    }
}
