using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale=0;
        gameIsPaused=true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale=1;
        gameIsPaused=false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
