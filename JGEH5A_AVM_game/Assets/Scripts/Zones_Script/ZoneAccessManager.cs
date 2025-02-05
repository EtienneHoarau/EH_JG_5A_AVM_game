using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAccessManager : MonoBehaviour
{
    public static ZoneAccessManager instance;
    public static ZoneAccessManager Instance => instance;
    [SerializeField] private List<GameObject> zoneEnnemies = new List<GameObject>();

    private int ennemiesAliveCount = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void AddEnnemyIfAlive(GameObject ennemy)
    {
        if(ennemy != null)
        {
            ennemiesAliveCount++;
        }
    }

    private void CountEnnemies()
    {
        foreach(GameObject ennemy in zoneEnnemies)
        {
            AddEnnemyIfAlive(ennemy);
        }
    }

    public void RemoveEnnemy(GameObject ennemy)
    {
        zoneEnnemies.Remove(ennemy);
        ennemiesAliveCount--;
        //print("Ennemies alive count: " + ennemiesAliveCount);
    }

    private void DeleteAllDoors()
    {
        Destroy(transform.parent.Find("barrier").gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        CountEnnemies();
    }

    // Update is called once per frame
    void Update()
    {
        //print("Zone ennemies count: " + zoneEnnemies.Count);
        if(ennemiesAliveCount == 0)
        {
            // Open the door
            print("Open the door");
            DeleteAllDoors();
            Destroy(transform.parent.gameObject);
        }
    }
}
