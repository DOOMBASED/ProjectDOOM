using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class PlayerMaster : MonoBehaviour
{

    public delegate void GeneralEventHandler();
    public delegate void AmmoPickupEventHandler(string ammoName, int quantity);
    public delegate void PlayerHealthEventHandler(int healthChange);

    public event GeneralEventHandler EventInventoryChanged;
    public event GeneralEventHandler EventHandsEmpty;
    public event GeneralEventHandler EventAmmoChanged;
    public event AmmoPickupEventHandler EventPickedUpAmmo;
    public event PlayerHealthEventHandler EventPlayerHealthDecrease;
    public event PlayerHealthEventHandler EventPlayerHealthIncrease;

    public void CallEventInventoryChanged()
    {
        if (EventInventoryChanged != null)
        {
            EventInventoryChanged();
        }
    }

    public void CallEventHandsEmpty()
    {
        if (EventHandsEmpty != null)
        {
            EventHandsEmpty();
        }
    }

    public void CallEventAmmoChanged()
    {
        if (EventAmmoChanged != null)
        {
            EventAmmoChanged();
        }
    }

    public void CallEventPickedUpAmmo(string ammoName, int quantity)
    {
        if (EventPickedUpAmmo != null)
        {
            EventPickedUpAmmo(ammoName, quantity);
        }
    }

    public void CallEventPlayerHealthDecrease(int decrease)
    {
        if (EventPlayerHealthDecrease != null)
        {
            EventPlayerHealthDecrease(decrease);
        }
    }

    public void CallEventPlayerHealthIncrease(int increase)
    {
        if (EventPlayerHealthIncrease != null)
        {
            EventPlayerHealthIncrease(increase);
        }
    }
}
}