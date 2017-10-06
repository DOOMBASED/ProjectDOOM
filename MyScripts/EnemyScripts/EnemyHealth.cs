using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class EnemyHealth : MonoBehaviour
{

	private EnemyMaster enemyMaster;

	public int enemyHealth = 100;
	public float healthLow = 25;

	void OnEnable()
	{
		SetInitialReferences();
		enemyMaster.EventEnemyDecreaseHealth += DecreaseHealth;
		enemyMaster.EventEnemyIncreaseHealth += IncreaseHealth;
	}

	void OnDisable()
	{
		enemyMaster.EventEnemyDecreaseHealth -= DecreaseHealth;
		enemyMaster.EventEnemyIncreaseHealth -= IncreaseHealth;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		enemyMaster = GetComponent<EnemyMaster>();
	}

	void DecreaseHealth(int healthChange)
	{
		enemyHealth -= healthChange;
		if (enemyHealth < 0)
		{
			enemyHealth = 0;
			enemyMaster.CallEventEnemyDie();
			Destroy(gameObject, Random.Range(10, 20));
		}
		CheckHealthFraction();
	}

	void CheckHealthFraction()
	{
		if (enemyHealth <= healthLow && enemyHealth > 0)
		{
			enemyMaster.CallEventEnemyHealthLow();
		}
		else if (enemyHealth > healthLow)
		{
			enemyMaster.CallEventEnemyHealthRecovered();
		}
	}

	void IncreaseHealth(int healthChange)
	{
		enemyHealth += healthChange;
		if (enemyHealth > 100)
		{
			enemyHealth = 100;
		}
		CheckHealthFraction();
	}
}
}