using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Tuto : IA
{
    // Start is called before the first frame update
    private void Update()
    {
        // Check for sight and attack
        inSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (!inSightRange && !inAttackRange)
        {
            Patroling();
        }
        if (inSightRange && !inAttackRange)
        {

            ChasePlayer();
        }
        
    }

}

