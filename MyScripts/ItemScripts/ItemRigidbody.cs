using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemRigidbody : MonoBehaviour
{

    private ItemMaster itemMaster;

    public Rigidbody[] rigidbodies;

    void OnEnable()
    {
        SetInitialReferences();
        itemMaster.EventObjectThrow += SetIsKinematicToFalse;
        itemMaster.EventObjectPickup += SetIsKinematicToTrue;
    }

    void OnDisable()
    {
        itemMaster.EventObjectThrow -= SetIsKinematicToFalse;
        itemMaster.EventObjectPickup -= SetIsKinematicToTrue;
    }

    void Start()
    {
        SetInitialReferences();
        CheckIfStartsInInventory();
    }

    void SetInitialReferences()
    {
        itemMaster = GetComponent<ItemMaster>();
    }

    void CheckIfStartsInInventory()
    {
        if (transform.root.CompareTag(GameManagerReferences._playerTag))
        {
            SetIsKinematicToTrue();
        }
    }

    void SetIsKinematicToTrue()
    {
        if (rigidbodies.Length > 0)
        {
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = true;
            }
        }
    }

    void SetIsKinematicToFalse()
    {
        if (rigidbodies.Length > 0)
        {
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = false;
            }
        }
    }
}
}