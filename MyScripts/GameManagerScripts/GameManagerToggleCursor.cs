using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GameManagerToggleCursor : MonoBehaviour
{

    private GameManagerMaster gameManagerMaster;
    private bool isCursorLocked = true;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.MenuToggleEvent += ToggleCursorState;
        gameManagerMaster.InventoryUIToggleEvent += ToggleCursorState;
    }

    void OnDisable()
    {
        gameManagerMaster.MenuToggleEvent -= ToggleCursorState;
        gameManagerMaster.InventoryUIToggleEvent -= ToggleCursorState;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckIfCursorShouldBeLocked();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManagerMaster>();
    }

    void ToggleCursorState()
    {
        isCursorLocked = !isCursorLocked;
    }

    void CheckIfCursorShouldBeLocked()
    {
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
}
