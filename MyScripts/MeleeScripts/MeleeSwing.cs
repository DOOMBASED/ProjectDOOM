using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class MeleeSwing : MonoBehaviour
{

    private MeleeMaster meleeMaster;

    public Animator myAnimator;
    public Collider myCollider;
    public Rigidbody myRigidbody;

    void OnEnable()
    {
        SetInitialReferences();
        meleeMaster.EventPlayerInput += MeleeAttackActions;
    }

    void OnDisable()
    {
        meleeMaster.EventPlayerInput -= MeleeAttackActions;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        meleeMaster = GetComponent<MeleeMaster>();
    }

    void MeleeAttackActions()
    {
        myCollider.enabled = true;
        myRigidbody.isKinematic = false;
        myAnimator.SetTrigger("Attack");
    }

    void MeleeAttackComplete()
    {
        myCollider.enabled = false;
        myRigidbody.isKinematic = true;
        meleeMaster.isInUse = false;
    }
}
}