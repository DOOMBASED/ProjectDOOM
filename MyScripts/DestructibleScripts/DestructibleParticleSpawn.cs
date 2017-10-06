using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleParticleSpawn : MonoBehaviour
{

    private DestructibleMaster destructibleMaster;

    public GameObject explosionEffect;

    void OnEnable()
    {
        SetInitialReferences();
        destructibleMaster.EventDestroyMe += SpawnExplosion;
    }

    void OnDisable()
    {
        destructibleMaster.EventDestroyMe -= SpawnExplosion;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        destructibleMaster = GetComponent<DestructibleMaster>();
    }

    void SpawnExplosion()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}
}