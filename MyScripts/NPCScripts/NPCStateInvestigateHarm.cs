using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class NPCStateInvestigateHarm : NPCStateInterface
{

    private readonly NPCStatePattern nPC;
    private RaycastHit hit;
    private Vector3 lookAtTarget;
    private float offset = 0.3f;

    public NPCStateInvestigateHarm(NPCStatePattern nPCStatePattern)
    {
        nPC = nPCStatePattern;
    }

    public void UpdateState()
    {
        Look();
    }
    public void ToPatrolState()
    {
        nPC.currentState = nPC.patrolState;
    }
    public void ToAlertState()
    {
        nPC.currentState = nPC.alertState;
    }
    public void ToPursueState()
    {
        nPC.currentState = nPC.pursueState;
    }
    public void ToMeleeAttackState() { }
    public void ToRangeAttackState() { }

    void Look()
    {
        if (nPC.pursueTarget == null)
        {
            ToPatrolState();
            return;
        }
        CheckIfTargetIsInDirectSight();
    }

    void CheckIfTargetIsInDirectSight()
    {
        lookAtTarget = new Vector3(nPC.pursueTarget.position.x, nPC.pursueTarget.position.y + offset, nPC.pursueTarget.position.z);
        if (Physics.Linecast(nPC.head.position, lookAtTarget, out hit, nPC.sightLayers))
        {
            if (hit.transform.root == nPC.pursueTarget)
            {
                nPC.locationOfInterest = nPC.pursueTarget.position;
                GoToLocationOfInterest();
                if (Vector3.Distance(nPC.transform.position, lookAtTarget) <= nPC.sightRange)
                {
                    ToPursueState();
                }
            }
            else
            {
                ToAlertState();
            }
        }
        else
        {
            ToAlertState();
        }
    }

    void GoToLocationOfInterest()
    {
        nPC.meshRendererFlag.material.color = Color.black;
        if (nPC.myNavMeshAgent.enabled && nPC.locationOfInterest != Vector3.zero)
        {
            nPC.myNavMeshAgent.SetDestination(nPC.locationOfInterest);
            nPC.myNavMeshAgent.isStopped = false;
            nPC.nPCMaster.CallEventNPCWalkAnim();
            if (nPC.myNavMeshAgent.remainingDistance <= nPC.myNavMeshAgent.stoppingDistance)
            {
                nPC.locationOfInterest = Vector3.zero;
                ToPatrolState();
            }
        }
        else
        {
            ToPatrolState();
        }
    }
}
}