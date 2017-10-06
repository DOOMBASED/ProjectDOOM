using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCCollisionField : MonoBehaviour
{

    private NPCMaster nPCMaster;
    private Rigidbody rigidbodyStrikingMe;
    private int damageToApply;
    private float damageFactor = 0.1f;

    public float massRequirement = 50;
    public float speedRequirement = 5;

    void OnEnable()
    {
        SetInitialReferences();
        nPCMaster.EventNPCDie += DisableThis;
    }

    void OnDisable()
    {
        nPCMaster.EventNPCDie -= DisableThis;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        nPCMaster = transform.root.GetComponent<NPCMaster>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            rigidbodyStrikingMe = other.GetComponent<Rigidbody>();
            if (rigidbodyStrikingMe.mass >= massRequirement && rigidbodyStrikingMe.velocity.sqrMagnitude >= speedRequirement * speedRequirement)
            {
                damageToApply = (int)(rigidbodyStrikingMe.mass * rigidbodyStrikingMe.velocity.magnitude * damageFactor);
                nPCMaster.CallEventNPCDecreaseHealth(damageToApply);
            }
        }
    }

    void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
}