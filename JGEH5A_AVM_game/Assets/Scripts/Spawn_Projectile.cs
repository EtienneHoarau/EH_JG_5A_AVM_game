using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Projectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private float spawnInterval = 1.0f;
    private float timeSinceLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnProjectile();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnProjectile()
    {
        Vector3 spawnPosition = transform.position + transform.forward;
        Instantiate(projectilePrefab, spawnPosition, transform.rotation);
    }
}
