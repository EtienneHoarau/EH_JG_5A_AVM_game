using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Zones : MonoBehaviour
{
    //Check if the zone directory is empty, if so it means all the enemies are dead and the player won
    void Update()
    {
        if(transform.childCount == 0)
        {
            GameManager.Instance.VictoryScreen();
        }
    }
}
