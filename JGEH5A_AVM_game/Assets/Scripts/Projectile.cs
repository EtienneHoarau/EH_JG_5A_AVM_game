using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{

    private Rigidbody bulletRigidBody;
    [SerializeField] private GameObject shooter;
    [SerializeField] private GameObject explositionEffect;
    private GameObject player;
    private GameObject robot;
    private IA IArobot;
    private ModifiedThirdPersonController playerController;

    // Start is called before the first frame update
    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 30f; // Bullet's speed
        bulletRigidBody.velocity = transform.forward * speed;
        this.player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        // prevent the player and his ball from colliding
        if (other.gameObject != shooter)
        {
            GameObject NewVFX = Instantiate(explositionEffect, transform.position, Quaternion.identity);
            if (other.gameObject == player)
            {
                playerController = other.gameObject.GetComponent<ModifiedThirdPersonController>();
                playerController.TakeDamage();
            }

            //Prevent ennemies from attacking between them
            if (other.gameObject.tag == "Robot" || other.gameObject.tag == "Boss Robot")
            {
                IArobot = other.gameObject.GetComponent<IA>();
                if (IArobot != null && !(IArobot is IA_Tuto))
                {
                    IArobot.TakeDamage();
                }
            }
            if (NewVFX)
                Destroy(NewVFX, 1.0f);
            if (gameObject)
                Destroy(gameObject);
        }
    }

}
