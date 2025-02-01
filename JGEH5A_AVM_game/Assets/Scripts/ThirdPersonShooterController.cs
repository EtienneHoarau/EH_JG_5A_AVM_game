using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{

    // Aim and Shoot variables
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    private float normalSensibility = 1f;
    private float aimSensibility = 0.25f;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform debugmouseWorldPosition;
    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    private ModifiedThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    // animation IDs
    private int _animIDAim;
    private int _animIDAimBackward;

    private bool _hasAnimator;
    private Animator _animator;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ModifiedThirdPersonController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);
        AssignAnimationIDs();
        Debug.Log(_hasAnimator);
    }
    private void AssignAnimationIDs()
    {
        _animIDAim = Animator.StringToHash("Aim");
        _animIDAimBackward = Animator.StringToHash("AimBackward");
    }
    // Update is called once per frame
    void Update()
    {


        if (starterAssetsInputs.aim)
        {
            // Configuration of the animation
            if (_hasAnimator)
            {
                // if going backward, then trigger the aimbarckward animation.
                if(starterAssetsInputs.move.y < 0)
                {
                    _animator.SetBool(_animIDAim, false);
                    _animator.SetBool(_animIDAimBackward, true);
                }
                //else triger the aim walking animation
                else
                {
                    _animator.SetBool(_animIDAim, true);
                    _animator.SetBool(_animIDAimBackward, false);
                }
            }
            // Call the Aim function
            Aim();

            // Configuration of the camera
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensibility);
            thirdPersonController.SetRotateOnMove(false);
        }
        else
        {
            // Configuration of the animation
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDAim, false);
                _animator.SetBool(_animIDAimBackward, false);
            }
            // Configuration of the camera
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensibility);
            thirdPersonController.SetRotateOnMove(true);
        }
        // Shoot function
        if (starterAssetsInputs.shoot && starterAssetsInputs.aim)
        {
            Shoot();
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

    private void Shoot()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            // The direction is given by the difference between the position of the spawned Bullet and the cursor 
            var aimDirection = position - spawnBulletPosition.position;

            // Ignore the height difference.
            aimDirection.y = 0;
            // Creation of the bullet at spawnBulletPosition position with the rotation based on aimDirection  and a vector Vector3(0,1,0)
            Instantiate(bulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            starterAssetsInputs.shoot = false;

        }
    }


}
