using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GameManagerPanelInstructions : MonoBehaviour
{

    private GameManagerMaster gameManagerMaster;

    public GameObject panelInstructions;
    public GameObject panelFactionRelations;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.GameOverEvent += TurnOffPanelInstructions;
    }

    void OnDisable()
    {
        gameManagerMaster.GameOverEvent -= TurnOffPanelInstructions;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManagerMaster>();
    }

    void TurnOffPanelInstructions()
    {
        if (panelInstructions != null)
        {
            panelInstructions.SetActive(false);
        }
        if (panelFactionRelations != null)
        {
            panelFactionRelations.SetActive(false);
        }
    }
}
}