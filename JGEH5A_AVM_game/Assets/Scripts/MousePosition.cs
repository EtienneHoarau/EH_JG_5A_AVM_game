using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public static MousePosition Instance { get; private set; }

    [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // cast a ray to the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            transform.position = raycastHit.point;
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        if(Instance == null)
        {
            Debug.Log("MousePosition Object does not exist");
        }
        return Instance.GetMouseWorldPosition_Instance();
    }

    private Vector3 GetMouseWorldPosition_Instance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderLayerMask))
        {
            return raycastHit.point;
        }
        else return Vector3.zero;

    }
}
