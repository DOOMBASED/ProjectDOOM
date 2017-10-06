using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunNPCInput : MonoBehaviour
{

    private GunMaster gunMaster;
    private NPCMaster nPCMaster;
    private NPCStatePattern nPCStatePattern;
    private Transform myTransform;
    public LayerMask layersToDamage;
    private RaycastHit hit;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventNPCInput += NPCFireGun;
        if (nPCMaster != null)
        {
            nPCMaster.EventNPCRelationsChange += ApplyLayersToDamage;
        }
        ApplyLayersToDamage();
    }

    void OnDisable()
    {
        gunMaster.EventNPCInput -= NPCFireGun;
        if (nPCMaster != null)
        {
            nPCMaster.EventNPCRelationsChange -= ApplyLayersToDamage;
        }
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<GunMaster>();
        if (transform.root.GetComponent<NPCMaster>() != null)
        {
            nPCMaster = transform.root.GetComponent<NPCMaster>();
        }
        if (transform.root.GetComponent<NPCStatePattern>() != null)
        {
            nPCStatePattern = transform.root.GetComponent<NPCStatePattern>();
        }
        myTransform = transform;
    }
    void NPCFireGun(float randomness)
    {
        Vector3 startPosition = new Vector3(Random.Range(-randomness, randomness), Random.Range(-randomness, randomness), 0.5f);
        if (Physics.Raycast(myTransform.TransformPoint(startPosition), myTransform.forward, out hit, GetComponent<GunShoot>().range, layersToDamage))
        {
            if (hit.transform.GetComponent<NPCTakeDamage>() != null || hit.transform == GameManagerReferences._player.transform)
            {
                gunMaster.CallEventShotEnemy(hit, hit.transform);
            }
            else
            {
                gunMaster.CallEventShotDefault(hit, hit.transform);
            }
        }
    }

    void ApplyLayersToDamage()
    {
        Invoke("ObtainLayersToDamage", 0.3f);
    }

    void ObtainLayersToDamage()
    {
        if (nPCStatePattern != null)
        {
            layersToDamage = nPCStatePattern.myEnemyLayers;
        }
    }

}
}