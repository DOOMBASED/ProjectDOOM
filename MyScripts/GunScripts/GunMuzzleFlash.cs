using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunMuzzleFlash : MonoBehaviour
{

    private GunMaster gunMaster;

    public ParticleSystem muzzleFlash;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventPlayerInput += PlayMuzzleFlash;
        gunMaster.EventNPCInput += PlayMuzzleFlashForNPC;
    }

    void OnDisable()
    {
        gunMaster.EventPlayerInput -= PlayMuzzleFlash;
        gunMaster.EventNPCInput -= PlayMuzzleFlashForNPC;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
    }

    void PlayMuzzleFlash()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
    }

    void PlayMuzzleFlashForNPC(float dummy)
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
    }
}
}