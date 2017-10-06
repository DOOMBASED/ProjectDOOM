using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCRagdollActivation : MonoBehaviour
{

    private NPCMaster nPCMaster;
    private Rigidbody myRigidbody;
    private Collider myCollider;

    void OnEnable()
    {
        SetInitialReferences();
        nPCMaster.EventNPCDie += ActivateRagdoll;
    }

    void OnDisable()
    {
        nPCMaster.EventNPCDie -= ActivateRagdoll;
    }

    void Start()
    {
        SetInitialReferences();
    }


    void SetInitialReferences()
    {
        nPCMaster = transform.root.GetComponent<NPCMaster>();
        if (GetComponent<Collider>() != null)
        {
            myCollider = GetComponent<Collider>();
        }
        if (GetComponent<Rigidbody>() != null)
        {
            myRigidbody = GetComponent<Rigidbody>();
        }
    }

    void ActivateRagdoll()
    {
        if (myCollider != null)
        {
            myCollider.enabled = true;
            myCollider.isTrigger = false;
        }
        if (myRigidbody != null)
        {
            myRigidbody.isKinematic = false;
            myRigidbody.useGravity = true;
        }
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
}