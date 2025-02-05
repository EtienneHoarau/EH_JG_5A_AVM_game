using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Zone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Transform parentOfParent = transform.parent.parent;
            if (parentOfParent != null)
            {
                Transform zone1 = parentOfParent.Find("zone1");
                if (zone1 != null)
                {
                    zone1.gameObject.SetActive(true);
                }
                Transform barrier = parentOfParent.Find("barrier");
                if (barrier != null)
                {
                    barrier.gameObject.SetActive(true);
                }
            }
            Destroy(this.transform.parent.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
