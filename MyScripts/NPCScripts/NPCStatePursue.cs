using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class NPCStatePursue : NPCStateInterface
{
    private readonly NPCStatePattern nPC;
    private float capturedDistance;

    public NPCStatePursue(NPCStatePattern nPCStatePattern)
    {
        nPC = nPCStatePattern;
    }

    public void UpdateState()
    {
        Look();
        Pursue();
    }
    public void ToPatrolState()
    {
        KeepWalking();
        nPC.currentState = nPC.patrolState;
    }
    public void ToAlertState()
    {
        KeepWalking();
        nPC.currentState = nPC.alertState;
    }
    public void ToPursueState() { }
    public void ToMeleeAttackState()
    {
        nPC.currentState = nPC.meleeAttackState;
    }
    public void ToRangeAttackState()
    {
        nPC.currentState = nPC.rangeAttackState;
    }

    void Look()
    {
        if (nPC.pursueTarget == null)
        {
            ToPatrolState();
            return;
        }
        Collider[] colliders = Physics.OverlapSphere(nPC.transform.position, nPC.sightRange, nPC.myEnemyLayers);
        if (colliders.Length == 0)
        {
            nPC.pursueTarget = null;
            ToPatrolState();
            return;
        }
        capturedDistance = nPC.sightRange * 2;
        foreach (Collider col in colliders)
        {
            float distanceToTarg = Vector3.Distance(nPC.transform.position, col.transform.position);
            if (distanceToTarg < capturedDistance)
            {
                capturedDistance = distanceToTarg;
                nPC.pursueTarget = col.transform.root;
            }
        }
    }

    void Pursue()
    {
        nPC.meshRendererFlag.material.color = Color.red;

        if (nPC. myNavMeshAgent.enabled && nPC.pursueTarget != null)
        {
            nPC.myNavMeshAgent.SetDestination(nPC.pursueTarget.position);
            nPC.locationOfInterest = nPC.pursueTarget.position;
            KeepWalking();
            float distanceToTarget = Vector3.Distance(nPC.transform.position, nPC.pursueTarget.position);
            if (distanceToTarget <= nPC.rangeAttackDamage && distanceToTarget > nPC.meleeAttackRange)
            {
                if (nPC.hasRangeAttack)
                {
                    ToRangeAttackState();
                }
            }
            else if (distanceToTarget <= nPC.meleeAttackRange)
            {
                if (nPC.hasMeleeAttack)
                {
                    ToMeleeAttackState();
                }
                else if (nPC.hasRangeAttack)
                {
                    ToRangeAttackState();
                }
            }
        }
        else
        {
            ToAlertState();
        }
    }

    void KeepWalking()
    {
        nPC.myNavMeshAgent.isStopped = false;
        nPC.nPCMaster.CallEventNPCWalkAnim();
    }
}
}