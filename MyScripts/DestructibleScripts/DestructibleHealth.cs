using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleHealth : MonoBehaviour
{

    private DestructibleMaster destructibleMaster;
    private int startingHealth;
    private bool isExploding = false;

    public int health;

    void OnEnable()
    {
        SetInitialReferences();
        destructibleMaster.EventDecreaseHealth += DecreaseHealth;
    }

    void OnDisable()
    {
        destructibleMaster.EventDecreaseHealth -= DecreaseHealth;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        destructibleMaster = GetComponent<DestructibleMaster>();
        startingHealth = health;
    }

    void DecreaseHealth(int healthToDecrease)
    {
        health -= healthToDecrease;
        CheckIfHealthLow();
        if (health <= 0 && !isExploding)
        {
            isExploding = true;
            destructibleMaster.CallEventDestroyMe();
        }

    }

    void CheckIfHealthLow()
    {
        if (health <= startingHealth / 2)
        {
            destructibleMaster.CallEventHealthLow();
        }
    }
}
}