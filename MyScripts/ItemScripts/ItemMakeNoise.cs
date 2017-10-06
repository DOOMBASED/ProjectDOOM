using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemMakeNoise : MonoBehaviour
{

    private GameManagerNPCRelationsMaster nPCRelationsMaster;
    private Collider[] colliders;
    private float nextNoiseTime;

    public string playerTag = "Player";
    public LayerMask applicableNPCLayer;
    public float noiseRange = 30;
    public float noiseRate = 10;
    public float speedThreshold = 5;

    void OnEnable()
    {
        SetInitialReferences();
        CallUpdateLayers();
        if (nPCRelationsMaster != null)
        {
            nPCRelationsMaster.EventUpdateNPCRelationsEverywhere += CallUpdateLayers;
        }
    }

    void OnDisable()
    {
        if (nPCRelationsMaster != null)
        {
            nPCRelationsMaster.EventUpdateNPCRelationsEverywhere -= CallUpdateLayers;
        }
    }

    void SetInitialReferences()
    {
        if (FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerNPCRelationsMaster>() != null)
        {
            nPCRelationsMaster = FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerNPCRelationsMaster>();
        }
        if (playerTag == "")
        {
            playerTag = "Player";
        }
    }

    void OnCollisionEnter()
    {
        if (Time.time > nextNoiseTime)
        {
            nextNoiseTime = Time.time + noiseRate;
            if (GetComponent<Rigidbody>().velocity.magnitude > speedThreshold)
            {
                Distraction();
            }
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