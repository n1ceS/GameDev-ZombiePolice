using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class chaseState : FSM
{    private zombieStates z;
    
    public chaseState(zombieStates z)
    {
    this.z = z;
    }
    
    public void attackState()
    {
        z.currentState = z.attackState;
    }

    public void onTriggerEnter(Collision enemy)
    {
        throw new System.NotImplementedException();
    }

    public void patrolState()
    {
        Debug.Log("Impossible");
    }

    public void updateState() 
    {
        Watch();
        Follow();
    }

    void FSM.chaseState()
    {
        z.navMeshAgent.destination = z.chaseTarget.position;
        z.navMeshAgent.isStopped= false;
        if(z.navMeshAgent.remainingDistance <= z.meleeRange)
        {
            z.navMeshAgent.isStopped = true;
            attackState();
        }
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

    void Follow()
    {
        float distance = Vector3.Distance(z.transform.position, z.chaseTarget.transform.position);
        z.navMeshAgent.destination = z.chaseTarget.position;
        z.navMeshAgent.isStopped = false;
        if(distance <=z.meleeRange)
        {
            Debug.Log("ATAKUJEMY");
            attackState();
        }
    }
}
