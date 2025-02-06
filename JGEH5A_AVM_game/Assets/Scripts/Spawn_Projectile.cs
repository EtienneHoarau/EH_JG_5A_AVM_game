using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Projectile : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private float spawnInterval = 2.0f; // Intervalle entre chaque tir
    private float timeSinceLastSpawn; // Temps écoulé depuis le dernier tir
    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastSpawn = 0f;
        _audioManager = GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si le temps écoulé depuis le dernier tir est supérieur à l'intervalle
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            StartCoroutine(SpawnProjectile());
            timeSinceLastSpawn = 0f; // Réinitialiser le temps écoulé
        }
    }

    private IEnumerator SpawnProjectile()
    {
        // Instancier et stocker le projectile
        Vector3 spawnPosition = transform.position + new Vector3(0,0,1);

        // Calculation of the direction for each bullet
        Vector3 direction = (spawnPosition - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        var bullet = Instantiate(projectilePrefab, spawnPosition, rotation);
        _audioManager.PlaySFX(_audioManager.EnnemyBulletSound);

        // Appliquer la force dans la bonne direction
        bullet.GetComponent<Rigidbody>().AddForce(direction * 1.5f, ForceMode.Impulse);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 0.05f, ForceMode.Impulse);
        bullet.GetComponent<Rigidbody>().AddForce(transform.up * 8f, ForceMode.Impulse);
        yield return new WaitForSeconds(3f); // Wait 3 seconds before the next spawn
    }
}
