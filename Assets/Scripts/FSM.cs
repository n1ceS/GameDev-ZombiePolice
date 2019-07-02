using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FSM
{
    void updateState();

    void onTriggerEnter(Collision enemy);

    void patrolState(); //PATROLOWANIE

    void attackState(); //ATAKOWANIE

    void chaseState(); //GONIENIE
}
