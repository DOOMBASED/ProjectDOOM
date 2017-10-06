using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCSetMyAttacker : MonoBehaviour
{

    private NPCStatePattern nPCStatePattern;
    private GameManagerNPCRelationsMaster nPCRelationsMaster;
    private int factionChangeAmount = 2;

    public bool applyRelationChainEffect = false;

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        nPCStatePattern = GetComponent<NPCStatePattern>();
        if (FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerNPCRelationsMaster>() != null)
        {
            nPCRelationsMaster = FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerNPCRelationsMaster>();
        }
    }

    public void SetMyAttacker(Transform attacker)
    {
        nPCStatePattern.myAttacker = attacker;
        if (nPCRelationsMaster != null)
        {
            nPCRelationsMaster.CallEventNPCRelationChange(transform.tag, attacker.tag, -factionChangeAmount, applyRelationChainEffect);
        }
    }
}
}