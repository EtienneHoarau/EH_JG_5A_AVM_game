using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class OnFall : MonoBehaviour
{
    private GameObject player;
    private GameManager gameManager;
    private void OnTriggerEnter(Collider other)
    {
        this.player = GameObject.FindWithTag("Player");
        // prevent the player and his ball from colliding
        if (other.gameObject == player)
        {
            GameManager.Instance.DefeatScreen();
        }
    }
}
