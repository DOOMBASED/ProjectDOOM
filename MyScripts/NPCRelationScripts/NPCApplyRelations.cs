using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCApplyRelations : MonoBehaviour
{

    private NPCMaster nPCMaster;
    private GameManagerNPCRelationsMaster nPCRelationsMaster;
    private NPCStatePattern nPCStatePattern;


    void OnEnable()
    {
        SetInitialReferences();
        nPCRelationsMaster.EventUpdateNPCRelationsEverywhere += SetMyRelations;
        Invoke("SetMyRelations", 0.1f);
    }

    void OnDisable()
    {
        nPCRelationsMaster.EventUpdateNPCRelationsEverywhere -= SetMyRelations;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        nPCMaster = GetComponent<NPCMaster>();
        nPCRelationsMaster = FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerNPCRelationsMaster>();
        nPCStatePattern = GetComponent<NPCStatePattern>();
    }

    void SetMyRelations()
    {
        if (nPCRelationsMaster.nPCRelationsArray == null)
        {
            return;
        }
        foreach (NPCRelationsArray nPCArray in nPCRelationsMaster.nPCRelationsArray)
        {
            if (transform.CompareTag(nPCArray.nPCFaction))
            {
                nPCStatePattern.myFriendlyLayers = nPCArray.myFriendlyLayers;
                nPCStatePattern.myEnemyLayers = nPCArray.myEnemyLayers;
                nPCStatePattern.myFriendlyTags = nPCArray.myFriendlyTags;
                nPCStatePattern.myEnemyTags = nPCArray.myEnemyTags;
                ApplySightLayers(nPCStatePattern.myFriendlyTags);
                CheckThatMyFollowTargetIsStillAnAlly(nPCStatePattern.myEnemyTags);
                nPCMaster.CallEventNPCRelationsChange();
                break;
            }
        }
    }

    void ApplySightLayers(string[] friendlyTags)
    {
        nPCStatePattern.sightLayers = LayerMask.NameToLayer("Everything");
        if (friendlyTags.Length > 0)
        {
            foreach (string fTag in friendlyTags)
            {
                int tempINT = LayerMask.NameToLayer(fTag);
                nPCStatePattern.sightLayers = ~(1 << tempINT | 1 << LayerMask.NameToLayer("Ignore Raycast"));
            }
        }
    }

    void CheckThatMyFollowTargetIsStillAnAlly(string[] enemyTags)
    {
        if (nPCStatePattern.myFollowTarget == null)
        {
            return;
        }
        if (enemyTags.Length > 0)
        {
            foreach (string eTag in enemyTags)
                if (nPCStatePattern.myFollowTarget.CompareTag(eTag))
                {
                    nPCStatePattern.myFollowTarget = null;
                    break;
                }
        }
    }
}
}