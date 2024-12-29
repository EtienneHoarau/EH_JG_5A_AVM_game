using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{

    private Rigidbody bulletRigidBody;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject explositionEffect;
    // Start is called before the first frame update
    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();      
    }

    private void Start()
    {
        float speed = 30f; // Bullet's speed
        bulletRigidBody.velocity = transform.forward * speed;
    }

    private IEnumerator ProjectileVFXManager(GameObject VFX)
    {
        GameObject NewVFX = Instantiate(VFX, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Debug.Log("Destroying VFX");
        Destroy(NewVFX, 1.0f);
    }
    private void OnTriggerEnter(Collider other)
    { // prevent the player and his ball from colliding
        if (other.gameObject != player)
        {
            GameObject NewVFX = Instantiate(explositionEffect, transform.position, Quaternion.identity);
           // StartCoroutine(ProjectileVFXManager(explositionEffect));
            Destroy(NewVFX, 1.0f);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
