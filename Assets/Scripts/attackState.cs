using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class attackState : FSM
{
    private zombieStates z;
    float timer=0;
    public attackState(zombieStates z)
    {
        this.z = z;
    }
    public void chaseState()
    {
        z.currentState = z.chaseState;
    }

    public void onTriggerEnter(Collision enemy)
    {
        throw new System.NotImplementedException();
    }

    public void patrolState()
    {
        Debug.Log("impossible");
    }

    public void updateState()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(z.chaseTarget.transform.position, z.transform.position);
        if(distance > z.meleeRange)
        {
            chaseState();
            z.navMeshAgent.isStopped = false;
        }
        Watch();
        if(distance <=z.meleeRange)
        {   
            z.chaseTarget.SendMessage("enemyHit", z.meleeDamage, SendMessageOptions.DontRequireReceiver);
           
            z.navMeshAgent.isStopped = true;
        }
    }

    void FSM.attackState()
    {
        Debug.Log("Error");
    }
    void Watch()
    {
        float distance = Vector3.Distance(z.transform.position, z.chaseTarget.transform.position);
        if (distance > z.patrolRange)
        {
            alertState();
        }
        else
        {
            z.lastPosition = z.chaseTarget.position;
        }
    }

    private void alertState()
    {
        z.currentState = z.alertState;
    }
  
}
