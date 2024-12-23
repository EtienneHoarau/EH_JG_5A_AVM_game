using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEditor.PackageManager;

public class ThirdPersonShooterController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    private float normalSensibility = 1f;
    private float aimSensibility = 0.25f;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform debugmouseWorldPosition;
    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    private ModifiedThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ModifiedThirdPersonController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (starterAssetsInputs.aim)
        {
            Aim();
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensibility);
            thirdPersonController.SetRotateOnMove(false);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensibility);
            thirdPersonController.SetRotateOnMove(true);
        }
        // A REVOIR
        if (starterAssetsInputs.shoot && starterAssetsInputs.aim)
        {
            var (success, position) = GetMousePosition();
            if (success)
            {
                // Calculate the direction
                // The direction is given by the difference between the position of the spawned Bullet and the cursor 
                var aimDirection = position - spawnBulletPosition.position;

                // Ignore the height difference.
                aimDirection.y = 0 ;
                // Creation of the bullet at spawnBulletPosition position with the rotation based on aimDirection  and a vector Vector3(0,1,0)
                Instantiate(bulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
                starterAssetsInputs.shoot = false;

            }
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimColliderLayerMask))
        {
            debugmouseWorldPosition.position = hitInfo.point;
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }


}
