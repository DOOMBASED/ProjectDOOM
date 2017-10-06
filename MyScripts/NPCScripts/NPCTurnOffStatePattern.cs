using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCTurnOffStatePattern : MonoBehaviour
{

    private NPCMaster nPCMaster;
    private NPCStatePattern nPCStatePattern;

    void OnEnable()
    {
        SetInitialReferences();
        nPCMaster.EventNPCDie += TurnOffStatePattern;
    }

    void OnDisable()
    {
        nPCMaster.EventNPCDie -= TurnOffStatePattern;
    }

    void Start()
    {
        SetInitialReferences();
    }


    void SetInitialReferences()
    {
        nPCMaster = GetComponent<NPCMaster>();
        if (GetComponent<NPCStatePattern>() != null)
        {
            nPCStatePattern = GetComponent<NPCStatePattern>();
        }
    }

    void TurnOffStatePattern()
    {
        if (nPCStatePattern != null)
        {
            nPCStatePattern.enabled = false;
        }
    }
}
}