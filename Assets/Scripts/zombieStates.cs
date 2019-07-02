using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieStates : MonoBehaviour
{
    public Transform[] wayPoints;
    public int patrolRange;
    public int meleeRange;
    public Transform vision;
    public int stayAlarmed;
    public  float meleeDamage;
    public float meleeDelay;

    public Transform chaseTarget; //OBIEKT DO GONIENIA(Czyli My)
    [HideInInspector] public alertState alertState;
    [HideInInspector] public attackState attackState;
    [HideInInspector] public chaseState chaseState;
    [HideInInspector] public patrolState patrolState;
    [HideInInspector] public FSM currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Vector3 lastPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        alertState = new alertState(this);
        attackState = new attackState(this);
        patrolState = new patrolState(this);
        chaseState = new chaseState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    void Start()
    {
        currentState = patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.updateState(); 
    }
    private void onTriggerEnter(Collision other)
    {
        currentState.onTriggerEnter(other);
    }
    void ShootingFrom(Vector3 destination)
    {
        Debug.Log("STRZELAJOM");
        lastPosition = destination;
        currentState = alertState;
    }
}
