using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class EnemyNavFlee : MonoBehaviour
{

    private EnemyMaster enemyMaster;
    private NavMeshAgent myNavMeshAgent;
    private NavMeshHit navHit;
    private Transform myTransform;
    private Vector3 runPosition;
    private Vector3 directionToPlayer;
    private float checkRate;
    private float nextCheck;

    public bool isFleeing;
    public Transform fleeTarget;
    public float fleeRange = 50;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DisableThis;
        enemyMaster.EventEnemySetNavTarget += SetFleeTarget;
        enemyMaster.EventEnemyHealthLow += IShouldFlee;
        enemyMaster.EventEnemyHealthRecovered += IShouldStopFleeing;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
        enemyMaster.EventEnemySetNavTarget -= SetFleeTarget;
        enemyMaster.EventEnemyHealthLow -= IShouldFlee;
        enemyMaster.EventEnemyHealthRecovered -= IShouldStopFleeing;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            CheckIfIShouldFlee();
        }
    }

    void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        myTransform = transform;
        if (GetComponent<NavMeshAgent>() != null)
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }
        checkRate = Random.Range(0.3f, 0.4f);
    }

    void SetFleeTarget(Transform target)
    {
        fleeTarget = target;
    }

    void IShouldFlee()
    {
        isFleeing = true;
        if (GetComponent<EnemyNavPursue>() != null)
        {
            GetComponent<EnemyNavPursue>().enabled = false;
        }
    }

    void IShouldStopFleeing()
    {
        isFleeing = false;
        if (GetComponent<EnemyNavPursue>() != null)
        {
            GetComponent<EnemyNavPursue>().enabled = true;
        }
    }

    void CheckIfIShouldFlee()
    {
        if (isFleeing)
        {
            if (fleeTarget != null && !enemyMaster.isOnRoute && !enemyMaster.isNavPaused)
            {
                if (FleeTarget(out runPosition) && Vector3.Distance(myTransform.position, fleeTarget.position) < fleeRange)
                {
                    myNavMeshAgent.SetDestination(runPosition);
                    enemyMaster.CallEventEnemyWalking();
                    enemyMaster.isOnRoute = true;
                }
            }
        }
    }

    bool FleeTarget(out Vector3 result)
    {
        directionToPlayer = myTransform.position - fleeTarget.position;
        Vector3 checkPos = myTransform.position + directionToPlayer;
        if (NavMesh.SamplePosition(checkPos, out navHit, 1.0f, NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = myTransform.position;
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}
}