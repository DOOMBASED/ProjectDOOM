using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FPSSystem
{
public class GameManagerGoToMenuScene : MonoBehaviour
{

    private GameManagerMaster gameManagerMaster;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.GoToMenuSceneEvent += GoToMenuScene;
    }

    void OnDisable()
    {
        gameManagerMaster.GoToMenuSceneEvent -= GoToMenuScene;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManagerMaster>();
    }

    void GoToMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
}