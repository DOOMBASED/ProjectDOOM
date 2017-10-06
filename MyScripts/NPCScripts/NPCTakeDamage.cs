using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCTakeDamage : MonoBehaviour
{

    private NPCMaster nPCMaster;

    public bool shouldRemoveCollider;
    public int damageMultiplier = 1;


    void OnEnable()
    {
        SetInitialReferences();
        nPCMaster.EventNPCDie += RemoveThis;
    }

    void OnDisable()
    {
        nPCMaster.EventNPCDie -= RemoveThis;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        nPCMaster = transform.root.GetComponent<NPCMaster>();
    }

    public void ProcessDamage(int damage)
    {
        int damageToApply = damage * damageMultiplier;
        nPCMaster.CallEventNPCDecreaseHealth(damageToApply);
    }

    void RemoveThis()
    {
        if (shouldRemoveCollider)
        {
            if (GetComponent<Collider>() != null)
            {
                Destroy(GetComponent<Collider>());
            }
            if (GetComponent<Rigidbody>() != null)
            {
                Destroy(GetComponent<Rigidbody>());
            }
        }
        gameObject.layer = LayerMask.NameToLayer("Default");
        Destroy(this);
    }
}
}