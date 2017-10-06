using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemCollider : MonoBehaviour
{

    private ItemMaster itemMaster;

    public PhysicMaterial myPhysicMaterial;
    public Collider[] colliders;

    void OnEnable()
    {
        SetInitialReferences();
        itemMaster.EventObjectThrow += EnableColliders;
        itemMaster.EventObjectPickup += DisableColliders;
    }

    void OnDisable()
    {
        itemMaster.EventObjectThrow -= EnableColliders;
        itemMaster.EventObjectPickup -= DisableColliders;
    }

    void Start()
    {
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
            DisableColliders();
        }
    }

    void EnableColliders()
    {
        if (colliders.Length > 0)
            foreach (Collider col in colliders)
            {
                col.enabled = true;
                if (myPhysicMaterial != null)
                {
                    col.material = myPhysicMaterial;
                }
            }
    }

    void DisableColliders()
    {
        if (colliders.Length > 0)
            foreach (Collider col in colliders)
            {
                col.enabled = false;
            }
    }
}
}