using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleNPCRelations : MonoBehaviour
{

    private GameManagerNPCRelationsMaster nPCRelationsMaster;
    private string factionInstigating;

    public bool applyRelationChainEffect = true;
    public string factionAffected;
    public int relationChangeOnDestroy = -50;

    void Start()
    {
        SetInitialReferences();
    }

    void OnDestroy()
    {
        ApplyRelationChangeOnDestruction();
    }

    void SetInitialReferences()
    {
        if (FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerNPCRelationsMaster>() != null)
        {
            nPCRelationsMaster = FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerNPCRelationsMaster>();
        }
    }

    public void SetMyAttacker(Transform attacker)
    {
        factionInstigating = attacker.tag;
    }

    void ApplyRelationChangeOnDestruction()
    {
        if (factionInstigating == null)
        {
            return;
        }
        nPCRelationsMaster.CallEventNPCRelationChange(factionAffected, factionInstigating, relationChangeOnDestroy, applyRelationChainEffect);
    }
}
}