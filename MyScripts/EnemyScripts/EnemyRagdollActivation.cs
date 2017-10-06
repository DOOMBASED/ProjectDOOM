using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class EnemyRagdollActivation : MonoBehaviour
{

    private EnemyMaster enemyMaster;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDie += ActivateRagdoll;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDie -= ActivateRagdoll;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        enemyMaster = transform.root.GetComponent<EnemyMaster>();
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
        if (myRigidbody != null)
        {
            myRigidbody.isKinematic = false;
            myRigidbody.useGravity = true;
        }
        if (myCollider != null)
        {
            myCollider.isTrigger = false;
            myCollider.enabled = true;
        }
    }
}
}