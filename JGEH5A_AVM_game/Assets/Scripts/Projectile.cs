using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{

    private Rigidbody bulletRigidBody;
    [SerializeField] private GameObject shooter;
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
    private void OnTriggerEnter(Collider other)
    { 
        // prevent the player and his ball from colliding
        if (other.gameObject != shooter)
        {
            GameObject NewVFX = Instantiate(explositionEffect, transform.position, Quaternion.identity);
            Destroy(NewVFX, 1.0f);
            Destroy(gameObject);
        }
    }

}
