using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Rigidbody bulletRigidBody;
    [SerializeField] private GameObject player;

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
        if(other.gameObject != player)// prevent the player and his ball from colliding
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
