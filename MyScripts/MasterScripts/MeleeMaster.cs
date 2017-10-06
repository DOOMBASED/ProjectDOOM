using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class MeleeMaster : MonoBehaviour
{

    public delegate void GeneralEventHandler();
    public delegate void MeleeHitEventHandler(Collision hitCollision, Transform hitTransform);
    public event GeneralEventHandler EventPlayerInput;
    public event GeneralEventHandler EventMeleeReset;
    public event MeleeHitEventHandler EventHit;
    public bool isInUse;
    public float swingRate = 0.5f;

    public void CallEventPlayerInput()
    {
        if (EventPlayerInput != null)
        {
            EventPlayerInput();
        }
    }

    public void CallEventMeleeReset()
    {
        if (EventMeleeReset != null)
        {
            EventMeleeReset();
        }
    }

    public void CallEventHit(Collision hCollision, Transform hTransform)
    {
        if (EventHit != null)
        {
            EventHit(hCollision, hTransform);
        }
    }
}
}