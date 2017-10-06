using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GameManagerToggleMenu : MonoBehaviour
{

    private GameManagerMaster gameManagerMaster;

    public GameObject menu;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.GameOverEvent += ToggleMenu;
    }

    void OnDisable()
    {
        gameManagerMaster.GameOverEvent -= ToggleMenu;
    }

    void Start()
    {
        SetInitialReferences();
        ToggleMenu();
    }

    void Update()
    {
        CheckForMenuToggleRequest();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManagerMaster>();
    }

    void CheckForMenuToggleRequest()
    {
        if (Input.GetButtonUp("Cancel") && !gameManagerMaster.isGameOver && !gameManagerMaster.isInventoryUIOn)
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        if (menu != null)
        {
            menu.SetActive(!menu.activeSelf);
            gameManagerMaster.isMenuOn = !gameManagerMaster.isMenuOn;
            gameManagerMaster.CallEventMenuToggle();
        }
    }
}
}