using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensibility;
    [SerializeField] private float aimSensibility;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform debugmouseWorldPosition;

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
        Vector3 mouseWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugmouseWorldPosition.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensibility);
            thirdPersonController.SetRotateOnMove(false);

            // Get the position of the mouse
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;

            // The direction is given by the difference between the position of the target and the player
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            // Player rotate to face where you target
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensibility);
            thirdPersonController.SetRotateOnMove(true);
        }
    }


}
