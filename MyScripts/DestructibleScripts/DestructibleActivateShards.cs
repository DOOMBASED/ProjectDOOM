using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleActivateShards : MonoBehaviour
{

    private DestructibleMaster destructibleMaster;
    private float myMass;

    public bool shouldShardsDisappear;
    public GameObject shards;
    public string shardLayer = "Ignore Raycast";

    void OnEnable()
    {
        SetInitialReferences();
        destructibleMaster.EventDestroyMe += ActivateShards;
    }

    void OnDisable()
    {
        destructibleMaster.EventDestroyMe -= ActivateShards;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        destructibleMaster = GetComponent<DestructibleMaster>();
        if (GetComponent<Rigidbody>() != null)
        {
            myMass = GetComponent<Rigidbody>().mass;
        }
    }

    void ActivateShards()
    {
        if (shards != null)
        {
            shards.transform.parent = null;
            shards.SetActive(true);
            foreach (Transform shard in shards.transform)
            {
                shard.tag = "Untagged";
                shard.gameObject.layer = LayerMask.NameToLayer(shardLayer);
                shard.GetComponent<Rigidbody>().AddExplosionForce(myMass, transform.position, 40, 0, ForceMode.Impulse);
                if (shouldShardsDisappear)
                {
                    Destroy(shard.gameObject, 10);
                }
            }
        }
    }
}
}