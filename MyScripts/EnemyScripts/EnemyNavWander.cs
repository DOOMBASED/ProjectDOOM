﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class EnemyNavWander : MonoBehaviour
{

    private EnemyMaster enemyMaster;
    private NavMeshAgent myNavMeshAgent;
    private Transform myTransform;
    private NavMeshHit navHit;
    private Vector3 wanderTarget;
    private float wanderRange = 10;
    private float checkRate;
    private float nextCheck;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += DisableThis;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= DisableThis;
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
            CheckIfIShouldWander();
        }
    }

    void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        if (GetComponent<NavMeshAgent>() != null)
        {
            myNavMeshAgent = GetComponent<NavMeshAgent>();
        }
        checkRate = Random.Range(0.3f, 0.4f);
        myTransform = transform;
    }

    void CheckIfIShouldWander()
    {
        if (enemyMaster.myTarget == null && !enemyMaster.isOnRoute && !enemyMaster.isNavPaused)
        {
            if (RandomWanderTarget(myTransform.position, wanderRange, out wanderTarget))
            {
                myNavMeshAgent.SetDestination(wanderTarget);
                enemyMaster.isOnRoute = true;
                enemyMaster.CallEventEnemyWalking();
            }
        }
    }

    bool RandomWanderTarget(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * wanderRange;
        if (NavMesh.SamplePosition(randomPoint, out navHit, 1.0f, NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = center;
            return false;
        }
    }

    void DisableThis()
    {
        this.enabled = false;
    }
}
}