using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleTakeDamage : MonoBehaviour
{

    private DestructibleMaster destructibleMaster;


    void OnEnable()
    {
        SetInitialReferences();
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        destructibleMaster = GetComponent<DestructibleMaster>();
    }

    public void ProcessDamage(int damage)
    {
        destructibleMaster.CallEventDecreaseHealth(damage);
    }
}
}