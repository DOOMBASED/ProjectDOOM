using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunBurstFireIndicator : MonoBehaviour
{

    private GunMaster gunMaster;

    public GameObject burstFireIndicator;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventToggleBurstFire += ToggleIndicator;
    }

    void OnDisable()
    {
        gunMaster.EventToggleBurstFire -= ToggleIndicator;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
    }

    void ToggleIndicator()
    {
        if (burstFireIndicator != null)
        {
            burstFireIndicator.SetActive(!burstFireIndicator.activeSelf);
        }
    }
}
}