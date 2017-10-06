using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class MeleeHitEffects : MonoBehaviour
{

    private MeleeMaster meleeMaster;

    public GameObject defaultHitEffect;
    public GameObject enemyHitEffect;

    void OnEnable()
    {
        SetInitialReferences();
        meleeMaster.EventHit += SpawnHitEffect;
    }

    void OnDisable()
    {
        meleeMaster.EventHit -= SpawnHitEffect;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        meleeMaster = GetComponent<MeleeMaster>();
    }

    void SpawnHitEffect(Collision hitCollison, Transform hitTransform)
    {
        Quaternion quatAngle = Quaternion.LookRotation(hitCollison.contacts[0].normal);
        if (hitTransform.GetComponent<NPCTakeDamage>() != null)
        {
            Instantiate(enemyHitEffect, hitCollison.contacts[0].point, quatAngle);
        }
        else
        {
            Instantiate(defaultHitEffect, hitCollison.contacts[0].point, quatAngle);
        }
    }
}
}