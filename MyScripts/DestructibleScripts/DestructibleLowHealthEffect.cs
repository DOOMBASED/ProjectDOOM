using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleLowHealthEffect : MonoBehaviour
{

    private DestructibleMaster destructibleMaster;
    public GameObject[] lowHealthEffectGO;

    void OnEnable()
    {
        SetInitialReferences();
        destructibleMaster.EventHealthLow += TurnOnLowHealthEffect;
    }

    void OnDisable()
    {
        destructibleMaster.EventHealthLow -= TurnOnLowHealthEffect;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        destructibleMaster = GetComponent<DestructibleMaster>();
    }

    void TurnOnLowHealthEffect()
    {
        if (lowHealthEffectGO.Length > 0)
        {
            for (int i = 0; i < lowHealthEffectGO.Length; i++)
            {
                lowHealthEffectGO[i].SetActive(true);
            }
        }
    }
}
}