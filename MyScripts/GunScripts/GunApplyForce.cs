using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunApplyForce : MonoBehaviour
{

    private GunMaster gunMaster;
    private Transform myTransform;

    public float forceToApply = 300;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventShotDefault += ApplyForce;
    }

    void OnDisable()
    {
        gunMaster.EventShotDefault -= ApplyForce;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
        myTransform = transform;
    }

    void ApplyForce(RaycastHit hitPosition, Transform hitTransform)
    {
        if (hitTransform.GetComponent<Rigidbody>() != null)
        {
            hitTransform.GetComponent<Rigidbody>().AddForceAtPosition(-myTransform.forward * forceToApply, transform.position, ForceMode.Impulse);
        }
    }
}
}