using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class NPCStateFlee : NPCStateInterface
{

    private readonly NPCStatePattern nPC;
    private Vector3 directionToEnemy;
    private NavMeshHit navHit;

    public NPCStateFlee(NPCStatePattern nPCStatePattern)
    {
        nPC = nPCStatePattern;
    }

    public void UpdateState()
    {
        CheckIfIShouldFlee();
        CheckIfIShouldFight();
    }
    public void ToPatrolState()
    {
        KeepWalking();
        nPC.currentState = nPC.patrolState;
    }
    public void ToAlertState() { }
    public void ToPursueState() { }
    public void ToMeleeAttackState()
    {
        KeepWalking();
        nPC.currentState = nPC.meleeAttackState;
    }
    public void ToRangeAttackState() { }

    void CheckIfIShouldFlee()
    {
        nPC.meshRendererFlag.material.color = Color.gray;
        Collider[] colliders = Physics.OverlapSphere(nPC.transform.position, nPC.sightRange, nPC.myEnemyLayers);
        if (colliders.Length == 0)
        {
            ToPatrolState();
            return;
        }
        directionToEnemy = nPC.transform.position - colliders[0].transform.position;
        Vector3 checkPos = nPC.transform.position.normalized + directionToEnemy;
        if (NavMesh.SamplePosition(checkPos, out navHit, 3.0f, NavMesh.AllAreas))
        {
            nPC.myNavMeshAgent.destination = navHit.position;
            KeepWalking();
        }
        else
        {
            StopWalking();
        }
    }

    void CheckIfIShouldFight()
    {
        if (nPC.pursueTarget == null)
        {
            return;
        }
        float distanceToTarget = Vector3.Distance(nPC.transform.position, nPC.pursueTarget.position);
        if (nPC.hasMeleeAttack && distanceToTarget <= nPC.meleeAttackRange)
        {
            ToMeleeAttackState();
        }
    }

    void KeepWalking()
    {
        nPC.myNavMeshAgent.isStopped = false;
        nPC.nPCMaster.CallEventNPCWalkAnim();
    }

    void StopWalking()
    {
        nPC.myNavMeshAgent.isStopped = true;
        nPC.nPCMaster.CallEventNPCIdleAnim();
    }
}
}