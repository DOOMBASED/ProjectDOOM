using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemTransparency : MonoBehaviour
{

    private ItemMaster itemMaster;
    private Material primaryMaterial;

    public Material transparentMaterial;

    void OnEnable()
    {
        SetInitialReferences();
        itemMaster.EventObjectPickup += SetToTransparentMaterial;
        itemMaster.EventObjectThrow += SetToPrimaryMaterial;
    }

    void OnDisable()
    {
        itemMaster.EventObjectPickup -= SetToTransparentMaterial;
        itemMaster.EventObjectThrow -= SetToPrimaryMaterial;
    }

    void Start()
    {
        SetInitialReferences();
        CaptureStartingMaterial();
    }

    void SetInitialReferences()
    {
        itemMaster = GetComponent<ItemMaster>();
    }

    void CaptureStartingMaterial()
    {
        primaryMaterial = GetComponent<Renderer>().material;
    }

    void SetToPrimaryMaterial()
    {
        GetComponent<Renderer>().material = primaryMaterial;
    }

    void SetToTransparentMaterial()
    {
        GetComponent<Renderer>().material = transparentMaterial;
    }
}
}