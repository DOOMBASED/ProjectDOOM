using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCStateStruck : NPCStateInterface
{

    private readonly NPCStatePattern nPC;
    private Collider[] colliders;
    private Collider[] friendlyColliders;
    private float informRate = 0.5f;
    private float nextInform;

    public NPCStateStruck(NPCStatePattern nPCStatePattern)
    {
        nPC = nPCStatePattern;
    }

    public void UpdateState()
    {
        InformNearbyAlliesThatIHaveBeenHurt();
    }
    public void ToPatrolState() { }
    public void ToAlertState()  { }
    public void ToPursueState() { }
    public void ToMeleeAttackState() { }
    public void ToRangeAttackState() { }

    void InformNearbyAlliesThatIHaveBeenHurt()
    {
        if (Time.time > nextInform)
        {
            nextInform = Time.time + informRate;
        }
        else
        {
            return;
        }
        if (nPC.myAttacker != null)
        {
            friendlyColliders = Physics.OverlapSphere(nPC.transform.position, nPC.sightRange, nPC.myFriendlyLayers);
            if (IsAttackerClose())
            {
                AlertNearbyAllies();
                SetMyselfToInvestigate();
            }
        }
    }

    bool IsAttackerClose()
    {
        if (Vector3.Distance(nPC.transform.position, nPC.myAttacker.position) <= nPC.sightRange * 2)
        {
            return true;
        }
        else
        {
            return false;

        }
    }

    void AlertNearbyAllies()
    {
        foreach (Collider ally in friendlyColliders)
        {
            if (ally.transform.root.GetComponent<NPCStatePattern>() != null)
            {
                NPCStatePattern allyPattern = ally.transform.root.GetComponent<NPCStatePattern>();
                if (allyPattern.currentState == allyPattern.patrolState)
                {
                    allyPattern.pursueTarget = nPC.myAttacker;
                    allyPattern.locationOfInterest = nPC.myAttacker.position;
                    allyPattern.currentState = allyPattern.investigateHarmState;
                    allyPattern.nPCMaster.CallEventNPCWalkAnim();
                }
            }
        }
    }

    void SetMyselfToInvestigate()
    {
        nPC.pursueTarget = nPC.myAttacker;
        nPC.locationOfInterest = nPC.myAttacker.position;
        if (nPC.capturedState == nPC.patrolState)
        {
            nPC.capturedState = nPC.investigateHarmState;
        }
    }
}
}