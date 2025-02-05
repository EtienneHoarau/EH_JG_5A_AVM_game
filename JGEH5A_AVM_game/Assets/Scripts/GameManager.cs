using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject VictoryUI;
    [SerializeField] private GameObject EnnemyList;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button restartButton2;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button mainMenuButton2;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button quitButton2;

    private StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogError($"Only one instance of {this.name} authorized !");
    }


    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        restartButton.onClick.AddListener(Reset);
        mainMenuButton.onClick.AddListener(MainMenuScene);
        quitButton.onClick.AddListener(Quit);

    }

    // Update is called once per frame
    void Update()
    {
        // if(EnnemyList.gameObject.transform.childCount == 0)
        // {
        //     VictoryScreen();
        // }
    }

    public void DeathScreen()
    {
        GameOverUI.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }
    public void VictoryScreen()
    {
        VictoryUI.SetActive(true);
        restartButton2.gameObject.SetActive(true);
        quitButton2.gameObject.SetActive(true);
        mainMenuButton2.gameObject.SetActive(true);
    }

    public void FinalVictoryScreen()
    {
        VictoryUI.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void VictoryButton()
    {
        SceneManager.LoadScene("BossMap Etienne");
    }

}
