using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GameManagerGameOver : MonoBehaviour
{

    private GameManagerMaster gameManagerMaster;

    public GameObject panelGameOver;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.GameOverEvent += TurnOnGameOverPanel;
    }

    void OnDisable()
    {
        gameManagerMaster.GameOverEvent -= TurnOnGameOverPanel;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManagerMaster>();
    }

    void TurnOnGameOverPanel()
    {
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }
    }
}
}
