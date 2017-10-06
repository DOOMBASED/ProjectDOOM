using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class MeleeReset : MonoBehaviour
{

    private MeleeMaster meleeMaster;
    private ItemMaster itemMaster;

    void OnEnable()
    {
        SetInitialReferences();
        if (itemMaster != null)
        {
            ResetMelee();
            itemMaster.EventObjectThrow += ResetMelee;
        }
    }

    void OnDisable()
    {
        if (itemMaster != null)
        {
            itemMaster.EventObjectThrow -= ResetMelee;
        }
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        meleeMaster = GetComponent<MeleeMaster>();
        if (GetComponent<ItemMaster>() != null)
        {
            itemMaster = GetComponent<ItemMaster>();
        }
    }

    void ResetMelee()
    {
        meleeMaster.isInUse = false;
    }
}
}