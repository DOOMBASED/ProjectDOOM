using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace FPSSystem
{
public class GameManagerTogglePlayer : MonoBehaviour
{

    private GameManagerMaster gameManagerMaster;

    public FirstPersonController playerController;
    public GameObject weaponCamera;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.MenuToggleEvent += TogglePlayerController;
        gameManagerMaster.InventoryUIToggleEvent += TogglePlayerController;
    }

    void OnDisable()
    {
        gameManagerMaster.MenuToggleEvent -= TogglePlayerController;
        gameManagerMaster.InventoryUIToggleEvent -= TogglePlayerController;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManagerMaster>();
    }

    void TogglePlayerController()
    {
        if (playerController != null)
        {
            playerController.enabled = !playerController.enabled;
            if (playerController.enabled)
            {
                weaponCamera.SetActive(true);
            }
            else
            {
                weaponCamera.SetActive(false);
            }
        }
    }
}
}