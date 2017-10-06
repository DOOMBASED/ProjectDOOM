using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCHeadLook : MonoBehaviour
{

    private NPCStatePattern nPCStatePattern;
    private Animator myAnimator;

    void OnEnable()
    {
        SetInitialReferences();
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        nPCStatePattern = GetComponent<NPCStatePattern>();
        myAnimator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if (myAnimator.enabled)
        {
            if (nPCStatePattern.pursueTarget != null)
            {
                myAnimator.SetLookAtWeight(1, 0, 0.5f, 0.5f, 0.7f);
                myAnimator.SetLookAtPosition(nPCStatePattern.pursueTarget.position);
            }
            else
            {
                myAnimator.SetLookAtWeight(0);
            }
        }
    }
}
}