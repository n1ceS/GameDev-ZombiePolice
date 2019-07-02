using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class alertState : FSM
{
    private zombieStates z;
    float timer = 0;
    public alertState(zombieStates z)
    {
        this.z = z;
    }
    public void attackState()
    {
        Debug.Log("Impossible");
    }

    public void chaseState()
    {
        z.currentState = z.chaseState;
    }

    public void onTriggerEnter(Collision enemy)
    {
        Debug.Log("UTKNALEM");
    }

    public void patrolState()
    {
        z.currentState = z.patrolState;
    }

    public void updateState()
    {
        Find();
        Watch();
        if (z.navMeshAgent.remainingDistance <= z.navMeshAgent.stoppingDistance)
        {
            LookAround();
        }
        }

    private void Find()
    {
        z.navMeshAgent.destination = z.lastPosition;
        z.navMeshAgent.isStopped =false;
    }
    void Watch()
    {
        float distance = Vector3.Distance(z.transform.position, z.chaseTarget.transform.position);
        if (distance <= z.patrolRange)
        {
            chaseState();
        }
    }
    void LookAround()
    {
        timer += Time.deltaTime;
        if(timer >= z.stayAlarmed)
        {
            timer = 0;
            patrolState();
        }
    }
}
