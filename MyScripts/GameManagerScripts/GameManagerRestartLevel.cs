using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FPSSystem
{
public class GameManagerRestartLevel : MonoBehaviour
{

    private GameManagerMaster gameManagerMaster;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.RestartLevelEvent += RestartLevel;
    }

    void OnDisable()
    {
        gameManagerMaster.RestartLevelEvent -= RestartLevel;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManagerMaster>();
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
}