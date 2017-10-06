using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GameManagerToggleInventoryUI : MonoBehaviour
{
    private GameManagerMaster gameManagerMaster;

    public bool hasInventory;
    public GameObject inventoryUI;
    public string toggleInventoryButton;

    void OnEnable()
    {
        SetInitialReferences();
    }

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckForInventoryUIToggleRequest();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManagerMaster>();
        if (toggleInventoryButton == "")
        {
            this.enabled = false;
        }
    }

    void CheckForInventoryUIToggleRequest()
    {
        if (Input.GetButtonUp(toggleInventoryButton) && !gameManagerMaster.isMenuOn && !gameManagerMaster.isGameOver && hasInventory)
        {
            ToggleInventoryUI();
        }
    }

    public void ToggleInventoryUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            gameManagerMaster.isInventoryUIOn = !gameManagerMaster.isInventoryUIOn;
            gameManagerMaster.CallEventInventoryUIToggle();
        }
    }
}
}
