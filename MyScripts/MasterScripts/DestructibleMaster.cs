using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleMaster : MonoBehaviour
{

    public delegate void GeneralEventHandler();
    public delegate void HealthEventHandler(int health);
    public event GeneralEventHandler EventDestroyMe;
    public event GeneralEventHandler EventHealthLow;
    public event HealthEventHandler EventDecreaseHealth;

    public void CallEventDecreaseHealth(int healthToDecrease)
    {
        if (EventDecreaseHealth != null)
        {
            EventDecreaseHealth(healthToDecrease);
        }
    }

    public void CallEventDestroyMe()
    {
        if (EventDestroyMe != null)
        {
            EventDestroyMe();
        }
    }

    public void CallEventHealthLow()
    {
        if (EventHealthLow != null)
        {
            EventHealthLow();
        }
    }
}
}