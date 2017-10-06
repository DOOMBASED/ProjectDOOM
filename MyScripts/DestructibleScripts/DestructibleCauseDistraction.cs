using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleCauseDistraction : MonoBehaviour
{

    private GameManagerNPCRelationsMaster nPCRelationsMaster;
    private DestructibleMaster destructibleMaster;
    private Collider[] colliders;

    public string playerTag = "Player";
    public LayerMask applicableNPCLayer;
    public float noiseRange = 100;

    void OnEnable()
    {
        SetInitialReferences();
        CallUpdateLayers();
        destructibleMaster.EventDestroyMe += Distraction;
        if (nPCRelationsMaster != null)
        {
            nPCRelationsMaster.EventUpdateNPCRelationsEverywhere += CallUpdateLayers;
        }
    }

    void OnDisable()
    {
        destructibleMaster.EventDestroyMe -= Distraction;
        if (nPCRelationsMaster != null)
        {
            nPCRelationsMaster.EventUpdateNPCRelationsEverywhere -= CallUpdateLayers;
        }
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        destructibleMaster = GetComponent<DestructibleMaster>();
        if (FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerNPCRelationsMaster>() != null)
        {
            nPCRelationsMaster = FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerNPCRelationsMaster>();
        }
        if (playerTag == "")
        {
            playerTag = "Player";
        }
    }

    void Distraction()
    {
        colliders = Physics.OverlapSphere(transform.position, noiseRange, applicableNPCLayer);
        if (colliders.Length == 0)
        {
            return;
        }
        foreach (Collider col in colliders)
        {
            col.transform.root.SendMessage("Distract", transform.position, SendMessageOptions.DontRequireReceiver);
        }
    }

    void CallUpdateLayers()
    {
        Invoke("UpdateLayersToDistract", 0.1f);
    }

    void UpdateLayersToDistract()
    {
        if (nPCRelationsMaster == null)
        {
            return;
        }
        foreach (NPCRelationsArray nPCArray in nPCRelationsMaster.nPCRelationsArray)
        {
            if (nPCArray.nPCFaction == playerTag)
            {
                applicableNPCLayer = nPCArray.myEnemyLayers;
                break;
            }
        }
    }
}
}