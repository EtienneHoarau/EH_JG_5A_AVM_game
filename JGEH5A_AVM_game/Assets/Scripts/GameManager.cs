using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;
    [SerializeField] private GameObject GameOverUI;
    [SerializeField] private GameObject VictoryUI;
    [SerializeField] private GameObject EnnemyList;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnnemyList.gameObject.transform.childCount == 0)
        {
            VictoryScreen();
        }
    }

    public void DeathScreen()
    {
        GameOverUI.SetActive(true);
    }
    public void VictoryScreen()
    {
        VictoryUI.SetActive(true);
    }
}
