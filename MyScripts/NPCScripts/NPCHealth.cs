using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class NPCHealth : MonoBehaviour
{

    private NPCMaster nPCMaster;
    private bool healthCritical;
    private float healthLow = 25;

    public int nPCHealth = 100;
    public int nPCMaxHealth = 100;

    void OnEnable()
    {
        SetInitialReferences();
        nPCMaster.EventNPCDecreaseHealth += DecreaseHealth;
        nPCMaster.EventNPCIncreaseHealth += IncreaseHealth;
    }

    void OnDisable()
    {
        nPCMaster.EventNPCDecreaseHealth -= DecreaseHealth;
        nPCMaster.EventNPCIncreaseHealth -= IncreaseHealth;
    }

    void Start()
    {
        SetInitialReferences();
    }

    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.P))
    //    {
    //        nPCMaster.CallEventNPCIncreaseHealth(20);
    //    }
    //}

    void SetInitialReferences()
    {
        nPCMaster = GetComponent<NPCMaster>();
    }

    void DecreaseHealth (int healthChange)
    {
        nPCHealth -= healthChange;
        if (nPCHealth <= 0)
        {
            nPCHealth = 0;
            nPCMaster.CallEventNPCDie();
            Destroy(gameObject, Random.Range(10, 20));
        }
        CheckHealthFraction();
    }

    void IncreaseHealth (int healthChange)
    {
        nPCHealth += healthChange;
        if (nPCHealth > nPCMaxHealth)
        {
            nPCHealth = nPCMaxHealth;
        }
    }

    void CheckHealthFraction()
    {

        if (nPCHealth <= healthLow && nPCHealth > 0)
        {
            nPCMaster.CallEventNPCLowHealth();
            healthCritical = true;
        }

        else if (nPCHealth > healthLow && healthCritical)
        {
            nPCMaster.CallEventNPCHealthRecovered();
            healthCritical = false;
        }
    }
}
}