using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleDegenerate : MonoBehaviour
{

    private DestructibleMaster destructibleMaster;
    private float nextDegenTime;
    private bool isHealthLow = false;

    public float degenRate = 1;
    public int healthLoss = 5;

    void OnEnable()
    {
        SetInitialReferences();
        destructibleMaster.EventHealthLow += HealthLow;
    }

    void OnDisable()
    {
        destructibleMaster.EventHealthLow -= HealthLow;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckIfHealthShouldDegenerate();
    }

    void SetInitialReferences()
    {
        destructibleMaster = GetComponent<DestructibleMaster>();
    }

    void HealthLow()
    {
        isHealthLow = true;
    }

    void CheckIfHealthShouldDegenerate()
    {
        if (isHealthLow)
        {
            if (Time.time > nextDegenTime)
            {
                nextDegenTime = Time.time + degenRate;
                destructibleMaster.CallEventDecreaseHealth(healthLoss);
            }
        }
    }
}
}