using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Zones : MonoBehaviour
{
    //Check if the zone directory is empty, if so it means all the enemies are dead and the player won
    void Update()
    {
        if(transform.childCount == 0)
        {
            if (SceneManager.GetActiveScene().name == "BossMap")
            {
                GameManager.Instance.FinalVictoryScreen();
            }
            else GameManager.Instance.VictoryScreen();
        }
    }
}
