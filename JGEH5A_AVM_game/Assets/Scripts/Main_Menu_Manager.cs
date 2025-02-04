using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        SceneManager.LoadScene("Map1 Etienne");
    }
}
