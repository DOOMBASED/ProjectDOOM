using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class NPCStateRangeAttack : NPCStateInterface
{

    private readonly NPCStatePattern nPC;
    private RaycastHit hit;

    public NPCStateRangeAttack(NPCStatePattern nPCStatePattern)
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
        nPC.pursueTarget = null;
        nPC.currentState = nPC.patrolState;
    }
    public void ToAlertState()
    {
        KeepWalking();
        nPC.currentState = nPC.alertState;
    }
    public void ToPursueState()
    {
        KeepWalking();
        nPC.currentState = nPC.pursueState;
    }
    public void ToMeleeAttackState()
    {
        nPC.currentState = nPC.meleeAttackState;
    }
    public void ToRangeAttackState() { }

    void TurnTowardsTarget()
    {
        Vector3 newPos = new Vector3(nPC.pursueTarget.position.x, nPC.transform.position.y, nPC.pursueTarget.position.z);
        nPC.transform.LookAt(newPos);
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
            ToPatrolState();
            return;
        }
        foreach (Collider col in colliders)
        {
            if (col.transform.root == nPC.pursueTarget)
            {
                TurnTowardsTarget();
                return;
            }
        }
        ToPatrolState();
    }

    void TryToAttack()
    {
        if (nPC.pursueTarget != null)
        {
            nPC.meshRendererFlag.material.color = Color.cyan;
            if (!IsTargetInSight())
            {
                ToPursueState();
                return;
            }
            if (Time.time > nPC.nextAttack)
            {
                nPC.nextAttack = Time.time + nPC.attackRate;
                float distanceToTarget = Vector3.Distance(nPC.transform.position, nPC.pursueTarget.position);
                TurnTowardsTarget();
                if (distanceToTarget <= nPC.rangeAttackRange)
                {
                    StopWalking();
                    if (nPC.rangeWeapon.GetComponent<GunMaster>() != null)
                    {
                        nPC.rangeWeapon.GetComponent<GunMaster>().CallEventNPCInput(nPC.rangeAttackSpread);
                        return;
                    }
                }
                if (distanceToTarget <= nPC.meleeAttackRange && nPC.hasMeleeAttack)
                {
                    ToMeleeAttackState();
                }
            }
        }
        else
        {
            ToPatrolState();
        }
    }

    void KeepWalking()
    {
        if (nPC.myNavMeshAgent.enabled)
        {
            nPC.myNavMeshAgent.isStopped = false;
            nPC.nPCMaster.CallEventNPCWalkAnim();
        }
    }

    void StopWalking()
    {
        if (nPC.myNavMeshAgent.enabled)
        {
            nPC.myNavMeshAgent.isStopped = true;
            nPC.nPCMaster.CallEventNPCIdleAnim();
        }
    }

    bool IsTargetInSight()
    {
        RaycastHit hit;

        Vector3 weaponLookAtVector = new Vector3(nPC.pursueTarget.position.x, nPC.pursueTarget.position.y + nPC.offset, nPC.pursueTarget.position.z);
        nPC.rangeWeapon.transform.LookAt(weaponLookAtVector);
        if (Physics.Raycast(nPC.rangeWeapon.transform.position, nPC.rangeWeapon.transform.forward, out hit))
        {
            foreach (string tag in nPC.myEnemyTags)
            {
                if (hit.transform.root.CompareTag(tag))
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }
}
}