using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPSSystem
{
public class PlayerHealth : MonoBehaviour
{

    private GameManagerMaster gameManagerMaster;
    private PlayerMaster playerMaster;

    public Text healthText;
    public int playerHealth;
    public int playerMaxHealth;

    void OnEnable()
    {
        SetInitialReferences();
        SetUI();
        playerMaster.EventPlayerHealthDecrease += DecreaseHealth;
        playerMaster.EventPlayerHealthIncrease += IncreaseHealth;
    }

    void OnDisable()
    {
        playerMaster.EventPlayerHealthDecrease -= DecreaseHealth;
        playerMaster.EventPlayerHealthIncrease -= IncreaseHealth;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = FindObjectOfType<GameManagerMaster>().GetComponent<GameManagerMaster>();
        playerMaster = GetComponent<PlayerMaster>();
    }

    void DecreaseHealth(int healthChange)
    {
        playerHealth -= healthChange;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            gameManagerMaster.CallEventGameOver();
        }
        SetUI();
    }

    void IncreaseHealth(int healthChange)
    {
        playerHealth += healthChange;
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
        SetUI();
    }

    void SetUI()
    {
        if (healthText != null)
        {
            healthText.text = playerHealth.ToString();
        }
    }
}
}