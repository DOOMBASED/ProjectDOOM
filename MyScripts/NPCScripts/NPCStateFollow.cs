using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class NPCStateFollow : NPCStateInterface
{
    private readonly NPCStatePattern nPC;
    private Collider[] colliders;
    private Vector3 lookAtPoint;
    private Vector3 heading;
    private float dotProd;

    public NPCStateFollow(NPCStatePattern nPCStatePattern)
    {
        nPC = nPCStatePattern;
    }

    public void UpdateState()
    {
        Look();
        FollowTarget();
    }
    public void ToPatrolState()
    {
        nPC.currentState = nPC.patrolState;
    }
    public void ToAlertState()
    {
        nPC.currentState = nPC.alertState;
    }
    public void ToPursueState() { }
    public void ToMeleeAttackState() { }
    public void ToRangeAttackState() { }

    void Look()
    {
        colliders = Physics.OverlapSphere(nPC.transform.position, nPC.sightRange / 3, nPC.myEnemyLayers);
        if (colliders.Length > 0)
        {
            AlertStateActions(colliders[0].transform);
            return;
        }
        colliders = Physics.OverlapSphere(nPC.transform.position, nPC.sightRange / 2, nPC.myEnemyLayers);
        if (colliders.Length > 0)
        {
            VisibilityCalculations(colliders[0].transform);
            if (dotProd > 0)
            {
                AlertStateActions(colliders[0].transform);
                return;
            }
        }
        colliders = Physics.OverlapSphere(nPC.transform.position, nPC.myEnemyLayers);
        foreach (Collider col in colliders)
        {
            RaycastHit hit;
            VisibilityCalculations(col.transform);
            if (Physics.Linecast(nPC.head.position, lookAtPoint, out hit, nPC.sightLayers))
            {
                foreach (string tags in nPC.myEnemyTags)
                {
                    if (hit.transform.CompareTag(tags))
                    {
                        if (dotProd > 0)
                        {
                            AlertStateActions(col.transform);
                            return;
                        }
                    }
                }
            }
        }
    }

    void FollowTarget()
    {
        nPC.meshRendererFlag.material.color = Color.blue;
        if (!nPC.myNavMeshAgent.enabled)
        {
            return;
        }
        if (nPC.myFollowTarget != null)
        {
            nPC.myNavMeshAgent.SetDestination(nPC.myFollowTarget.position);
            KeepWalking();
        }
        else
        {
            ToPatrolState();
        }
        if (HaveIReachedDestination())
        {
            StopWalking();
        }
    }

    void AlertStateActions(Transform target)
    {
        nPC.locationOfInterest = target.position;
        ToAlertState();
    }

    void VisibilityCalculations(Transform target)
    {
        lookAtPoint = new Vector3(target.position.x, target.position.y + nPC.offset, target.position.z);
        heading = lookAtPoint - nPC.transform.position;
        dotProd = Vector3.Dot(heading, nPC.transform.forward);
    }

    bool HaveIReachedDestination()
    {
        if (nPC.myNavMeshAgent.remainingDistance <= nPC.myNavMeshAgent.stoppingDistance && !nPC.myNavMeshAgent.pathPending)
        {
            StopWalking();
            return true;
        }
        else
        {
            KeepWalking();
            return false;
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