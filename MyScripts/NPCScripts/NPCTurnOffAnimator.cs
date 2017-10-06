using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCTurnOffAnimator : MonoBehaviour
{

    private NPCMaster nPCMaster;
    private Animator myAnimator;

    void OnEnable()
    {
        SetInitialReferences();
        nPCMaster.EventNPCDie += TurnOffAnimator;
    }

    void OnDisable()
    {
        nPCMaster.EventNPCDie -= TurnOffAnimator;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        nPCMaster = GetComponent<NPCMaster>();
        if (GetComponent<Animator>() != null)
        {
            myAnimator = GetComponent<Animator>();
        }
    }

    void TurnOffAnimator()
    {
        if (myAnimator != null)
        {
            myAnimator.enabled = false;
        }
    }
}
}