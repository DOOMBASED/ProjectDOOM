﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunMaster : MonoBehaviour
{

    public delegate void GeneralEventHandler();
    public delegate void GunHitEventHandler(RaycastHit hitPosition, Transform hitTransform);
    public delegate void GunAmmoEventHandler(int currentAmmo, int carriedAmmo);
    public delegate void GunCrosshairEventHandler(float speed);
    public delegate void GunNPCEventHandler(float random);
    public event GeneralEventHandler EventPlayerInput;
    public event GeneralEventHandler EventGunNotUsable;
    public event GeneralEventHandler EventRequestReload;
    public event GeneralEventHandler EventRequestGunReset;
    public event GeneralEventHandler EventToggleBurstFire;
    public event GunHitEventHandler EventShotDefault;
    public event GunHitEventHandler EventShotEnemy;
    public event GunAmmoEventHandler EventAmmoChanged;
    public event GunCrosshairEventHandler EventSpeedCaptured;
    public GunNPCEventHandler EventNPCInput;
    public bool isGunLoaded;
    public bool isReloading;

    public void CallEventPlayerInput()
    {
        if (EventPlayerInput != null)
        {
            EventPlayerInput();
        }
    }

    public void CallEventGunNotUsable()
    {
        if (EventGunNotUsable != null)
        {
            EventGunNotUsable();
        }
    }

    public void CallEventRequestReload()
    {
        if (EventRequestReload != null)
        {
            EventRequestReload();
        }
    }

    public void CallEventRequestGunReset()
    {
        if (EventRequestGunReset != null)
        {
            EventRequestGunReset();
        }
    }

    public void CallEventToggleBurstFire()
    {
        if (EventToggleBurstFire != null)
        {
            EventToggleBurstFire();
        }
    }

    public void CallEventShotDefault(RaycastHit hPos, Transform hTransform)
    {
        if (EventShotDefault != null)
        {
            EventShotDefault(hPos, hTransform);
        }
    }

    public void CallEventShotEnemy(RaycastHit hPos, Transform hTransform)
    {
        if (EventShotEnemy != null)
        {
            EventShotEnemy(hPos, hTransform);
        }
    }

    public void CallEventAmmoChanged(int curAmmo, int carAmmo)
    {
        if (EventAmmoChanged != null)
        {
            EventAmmoChanged(curAmmo, carAmmo);
        }
    }

    public void CallEventSpeedCaptured(float spd)
    {
        if (EventSpeedCaptured != null)
        {
            EventSpeedCaptured(spd);
        }
    }

    public void CallEventNPCInput(float rnd)
    {
        if (EventNPCInput != null)
        {
            EventNPCInput(rnd);
        }
    }
}
}