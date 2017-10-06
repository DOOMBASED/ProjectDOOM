using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCHoldRangeWeapon : MonoBehaviour
{

    private NPCStatePattern nPCStatePattern;
    private Animator myAnimator;

    public Transform rightHandTarget;
    public Transform leftHandTarget;

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
        if (nPCStatePattern.rangeWeapon == null)
        {
            return;
        }
        if (myAnimator.enabled)
        {
            if (nPCStatePattern.rangeWeapon.activeSelf)
            {
                if (rightHandTarget != null)
                {
                    myAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    myAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    myAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
                    myAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
                }
                if (leftHandTarget != null)
                {
                    myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    myAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
                    myAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);
                }
            }
        }
    }
}
}