using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class patrolState : FSM
{
    private zombieStates z;
    int nextWayPoint = 0; //nastepny punkt do patrolowania
    public patrolState(zombieStates zz)
    {
        z = zz;
    }
    public void attackState()
    {
        z.currentState = z.attackState;
    }

    public void chaseState()
    {
        z.currentState = z.chaseState;
    }

    public void onTriggerEnter(Collision enemy)
    {
        if(enemy.gameObject.CompareTag("Player"))
        {
            // alertState();
            Debug.Log("SMARKI");
        }
    }

    public void updateState()
    {
        Watch();
        Patrol();
    }

    void FSM.patrolState()
    {
        Debug.LogError("Patrolling now");
    }
    void Watch()
    {
        float distance = Vector3.Distance(z.transform.position, z.chaseTarget.transform.position);
        if(distance <= z.patrolRange)
        {
           // Debug.Log("MAM CIE");
            chaseState();
        }
        /*
        RaycastHit hit;
        if(Physics.Raycast(z.transform.position,z.transform.TransformDirection(Vector3.back),out hit,Mathf.Infinity))
        {
            Debug.Log("XD");
            if(hit.collider.CompareTag("Player"))
                {
                Debug.Log("MAM WROGA");
                z.chaseTarget = hit.transform;
                //chaseState();
            }
        }*/

    }
    void Patrol()
    {
        z.navMeshAgent.destination = z.wayPoints[nextWayPoint].position;
        z.navMeshAgent.isStopped=false;
        if(z.navMeshAgent.remainingDistance >= z.navMeshAgent.stoppingDistance )
        {
            //Debug.Log("XDD");
            nextWayPoint = (nextWayPoint+1)%z.wayPoints.Length;
        }
    }
    void Start()
    {
    }
}
