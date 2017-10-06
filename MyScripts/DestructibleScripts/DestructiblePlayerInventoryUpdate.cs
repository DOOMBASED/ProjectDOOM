using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructiblePlayerInventoryUpdate : MonoBehaviour
{

    private DestructibleMaster destructibleMaster;
    private PlayerMaster playerMaster;

    void OnEnable()
    {
        SetInitialReferences();
        destructibleMaster.EventDestroyMe += ForcePlayerInventoryUpdate;
    }

    void OnDisable()
    {
        destructibleMaster.EventDestroyMe -= ForcePlayerInventoryUpdate;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        if (GetComponent<ItemMaster>() == null)
        {
            Destroy(this);
        }
        if (GameManagerReferences._player != null)
        {
            playerMaster = GameManagerReferences._player.GetComponent<PlayerMaster>();
        }
        destructibleMaster = GetComponent<DestructibleMaster>();
    }

    void ForcePlayerInventoryUpdate()
    {
        if (playerMaster != null)
        {
            playerMaster.CallEventInventoryChanged();
        }
    }
}
}