using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Tuto : IA
{
    // Change the attitud of the IA from the tutorial, to prevent it from attacking the player
    private void Update()
    {
        // Check for sight and attack
        inSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        Patroling();        
    }

}

