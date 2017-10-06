using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class TestGameOver : MonoBehaviour
{

    private GameManagerMaster gameManagerMaster;

    void Start()
    {
        gameManagerMaster = GetComponent<GameManagerMaster>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            gameManagerMaster.CallEventGameOver();
        }
    }
}
}