using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemConvexMeshManagement : MonoBehaviour
{

    private ItemMaster itemMaster;
    private float checkRate = 0.2f;
    private float nextCheck;

    public bool isSettled = true;
    public Rigidbody myRigidbody;
    public MeshCollider[] meshColliders;

    void OnEnable()
    {
        SetInitialReferences();
        itemMaster.EventObjectPickup += EnableMeshConvex;
    }

    void OnDisable()
    {
        itemMaster.EventObjectPickup -= EnableMeshConvex;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckIfIHaveSettled();
    }

    void SetInitialReferences()
    {
        itemMaster = GetComponent<ItemMaster>();
    }

    void CheckIfIHaveSettled()
    {
        if (Time.time > nextCheck && !isSettled)
        {
            nextCheck = Time.time + checkRate;
            if (Mathf.Approximately(myRigidbody.velocity.magnitude, 0) && !myRigidbody.isKinematic)
            {
                isSettled = true;
                DisableMeshConvexAndEnableIsKinematic();
            }
        }
    }

    void EnableMeshConvex()
    {
        isSettled = false;
        if (meshColliders.Length > 0)
        {
            foreach (MeshCollider subMeshCollider in meshColliders)
            {
                subMeshCollider.convex = true;
            }
        }
    }

    void DisableMeshConvexAndEnableIsKinematic()
    {
        myRigidbody.isKinematic = true;
        if (meshColliders.Length > 0)
        {
            foreach (MeshCollider subMeshCollider in meshColliders)
            {
                subMeshCollider.convex = false;
            }
        }
    }
}
}