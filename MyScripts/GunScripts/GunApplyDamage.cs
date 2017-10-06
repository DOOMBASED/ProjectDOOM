using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunApplyDamage : MonoBehaviour
{

    private GunMaster gunMaster;

    public int damage = 5;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventShotEnemy += ApplyDamage;
        gunMaster.EventShotDefault += ApplyDamage;
    }

    void OnDisable()
    {
        gunMaster.EventShotEnemy -= ApplyDamage;
        gunMaster.EventShotDefault -= ApplyDamage;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
    }

    void ApplyDamage(RaycastHit hitPosition, Transform hitTransform)
    {
        hitTransform.SendMessage("ProcessDamage", damage, SendMessageOptions.DontRequireReceiver);
        hitTransform.SendMessage("CallEventPlayerHealthDecrease", damage, SendMessageOptions.DontRequireReceiver);
        hitTransform.root.SendMessage("SetMyAttacker", transform.root, SendMessageOptions.DontRequireReceiver);
    }
}
}