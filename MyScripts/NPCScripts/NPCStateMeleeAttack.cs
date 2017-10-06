using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class NPCStateMeleeAttack : NPCStateInterface
{

    private readonly NPCStatePattern nPC;
    private float distanceToTarget;

    public NPCStateMeleeAttack(NPCStatePattern nPCStatePattern)
    {
        nPC = nPCStatePattern;
    }

    public void UpdateState()
    {
        Look();
        TryToAttack();
    }
    public void ToPatrolState()
    {
        KeepWalking();
        nPC.isMeleeAttacking = false;
        nPC.currentState = nPC.patrolState;
    }
    public void ToAlertState() { }
    public void ToPursueState()
    {
        KeepWalking();
        nPC.isMeleeAttacking = false;
        nPC.currentState = nPC.pursueState;
    }
    public void ToMeleeAttackState() { }
    public void ToRangeAttackState() { }

    void Look()
    {
        if (nPC.pursueState == null)
        {
            ToPatrolState();
            return;
        }
        Collider[] colliders = Physics.OverlapSphere(nPC.transform.position, nPC.meleeAttackRange, nPC.myEnemyLayers);
        if (colliders.Length == 0)
        {
            ToPursueState();
            return;
        }
        foreach (Collider col in colliders)
        {
            if (col.transform.root == nPC.pursueTarget)
            {
                return;
            }
        }
        ToPursueState();
    }

    void TryToAttack()
    {
        if (nPC.pursueTarget != null)
        {
            nPC.meshRendererFlag.material.color = Color.magenta;
            if (Time.time > nPC.nextAttack && !nPC.isMeleeAttacking)
            {
                nPC.nextAttack = Time.time + nPC.attackRate;
                if (Vector3.Distance(nPC.transform.position, nPC.pursueTarget.position) <= nPC.meleeAttackRange)
                {
                    Vector3 newPos = new Vector3(nPC.pursueTarget.position.x, nPC.transform.position.y, nPC.pursueTarget.position.z);
                    nPC.transform.LookAt(newPos);
                    nPC.nPCMaster.CallEventNPCAttackAnim();
                    nPC.isMeleeAttacking = true;
                }
                else
                {
                    ToPursueState();
                }
            }
        }
        else
        {
            ToPursueState();
        }
    }

    void KeepWalking()
    {
        nPC.myNavMeshAgent.isStopped = false;
        nPC.nPCMaster.CallEventNPCWalkAnim();
    }
}
}