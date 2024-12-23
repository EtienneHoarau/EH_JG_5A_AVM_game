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
    [SerializeField] private float normalSensibility;
    [SerializeField] private float aimSensibility;
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
        //Vector3 mouseWorldPosition = Vector3.zero;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        //{
        //   debugmouseWorldPosition.position = raycastHit.point;
        //   mouseWorldPosition = raycastHit.point;
        //}
        Aim();
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensibility);
            thirdPersonController.SetRotateOnMove(false);

            // Get the position of the mouse
          //  Vector3 worldAimTarget = mouseWorldPosition;
           // worldAimTarget.y = transform.position.y;

            // The direction is given by the difference between the position of the target and the player
            //Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            // Player rotate to face where you target
            //transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensibility);
            thirdPersonController.SetRotateOnMove(true);
        }
        if (starterAssetsInputs.shoot)
        {
            var (success, position) = GetMousePosition();
            if (success)
            {
                // Calculate the direction
                var direction = position - spawnBulletPosition.position;

                // You might want to delete this line.
                // Ignore the height difference.
                direction.y = 0;
                // The direction is given by the difference between the position of the spawned Bullet and the cursor 
                Vector3 aimDirection = (position - spawnBulletPosition.position).normalized;
                // Creation of the bullet at spawnBulletPosition position with the rotation based on aimDirection  and a vector Vector3(0,1,0)
                Instantiate(bulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(direction, Vector3.up));
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
