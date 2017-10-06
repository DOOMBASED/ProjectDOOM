using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCMaster : MonoBehaviour
{
    public delegate void GeneralEventHandler();
    public delegate void HealthEventHandler(int health);
    public delegate void NPCRelationsChangeEventHandler();
    public event GeneralEventHandler EventNPCDie;
    public event GeneralEventHandler EventNPCLowHealth;
    public event GeneralEventHandler EventNPCHealthRecovered;
    public event GeneralEventHandler EventNPCWalkAnim;
    public event GeneralEventHandler EventNPCStruckAnim;
    public event GeneralEventHandler EventNPCAttackAnim;
    public event GeneralEventHandler EventNPCRecoveredAnim;
    public event GeneralEventHandler EventNPCIdleAnim;
    public event HealthEventHandler EventNPCDecreaseHealth;
    public event HealthEventHandler EventNPCIncreaseHealth;
    public event NPCRelationsChangeEventHandler EventNPCRelationsChange;
    public string animationBoolPursuing = "isPursuing";
    public string animationTriggerStruck = "Struck";
    public string animationTriggerMelee = "Attack";
    public string animationTriggerRecovered = "Recovered";

    public void CallEventNPCDie()
    {
        if (EventNPCDie != null)
        {
            EventNPCDie();
        }
    }

    public void CallEventNPCLowHealth()
    {
        if (EventNPCLowHealth != null)
        {
            EventNPCLowHealth();
        }
    }

    public void CallEventNPCHealthRecovered()
    {
        if (EventNPCHealthRecovered != null)
        {
            EventNPCHealthRecovered();
        }
    }

    public void CallEventNPCWalkAnim()
    {
        if (EventNPCWalkAnim != null)
        {
            EventNPCWalkAnim();
        }
    }

    public void CallEventNPCStruckAnim()
    {
        if (EventNPCStruckAnim != null)
        {
            EventNPCStruckAnim();
        }
    }

    public void CallEventNPCAttackAnim()
    {
        if (EventNPCAttackAnim != null)
        {
            EventNPCAttackAnim();
        }
    }

    public void CallEventNPCRecoveredAnim()
    {
        if (EventNPCRecoveredAnim != null)
        {
            EventNPCRecoveredAnim();
        }
    }

    public void CallEventNPCIdleAnim()
    {
        if (EventNPCIdleAnim != null)
        {
            EventNPCIdleAnim();
        }
    }

    public void CallEventNPCDecreaseHealth(int health)
    {
        if (EventNPCDecreaseHealth != null)
        {
            EventNPCDecreaseHealth(health);
        }
    }

    public void CallEventNPCIncreaseHealth(int health)
    {
        if (EventNPCIncreaseHealth != null)
        {
            EventNPCIncreaseHealth(health);
        }
    }

    public void CallEventNPCRelationsChange()
    {
        if (EventNPCRelationsChange != null)
        {
            EventNPCRelationsChange();
        }
    }
}
}