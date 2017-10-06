using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunReset : MonoBehaviour
{

    private GunMaster gunMaster;
    private ItemMaster itemMaster;

    void OnEnable()
    {
        SetInitialReferences();
        if (itemMaster != null)
        {
            itemMaster.EventObjectThrow += ResetGun;
        }
    }

    void OnDisable()
    {
        itemMaster.EventObjectThrow -= ResetGun;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
        if (GetComponent<ItemMaster>() != null)
        {
            itemMaster = GetComponent<ItemMaster>();
        }
    }

    void ResetGun()
    {
        gunMaster.CallEventRequestGunReset();
    }
}
}