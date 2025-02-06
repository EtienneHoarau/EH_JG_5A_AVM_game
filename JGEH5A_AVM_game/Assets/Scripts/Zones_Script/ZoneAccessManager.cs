using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAccessManager : MonoBehaviour
{
    // Singleton instance
    public static ZoneAccessManager instance;
    public static ZoneAccessManager Instance => instance;

    // List of enemy in the zone
    [SerializeField] private List<GameObject> zoneEnnemies = new List<GameObject>();

    // Count of alive enemies
    private int ennemiesAliveCount = 0;

    private void Awake()
    {
        // Initialize the singleton instance
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Adds an enemy to the count if it is alive
    private void AddEnnemyIfAlive(GameObject ennemy)
    {
        if(ennemy != null)
        {
            ennemiesAliveCount++;
        }
    }

    // Counts the number of alive enemies in the zone
    private void CountEnnemies()
    {
        foreach(GameObject ennemy in zoneEnnemies)
        {
            AddEnnemyIfAlive(ennemy);
        }
    }

    // Removes an enemy from the list and decreases the alive count
    public void RemoveEnnemy(GameObject ennemy)
    {
        zoneEnnemies.Remove(ennemy);
        ennemiesAliveCount--;
    }

    void Start()
    {
        // Count the initial number of alive enemies
        CountEnnemies();
    }

    void Update()
    {
        // Check if all enemies are dead to destroy the zone and free the player
        if(ennemiesAliveCount <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
