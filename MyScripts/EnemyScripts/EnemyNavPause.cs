using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class EnemyNavPause : MonoBehaviour
{

	private EnemyMaster enemyMaster;
	private NavMeshAgent myNavMeshAgent;
	private float pauseTime = 1;

	void OnEnable()
	{
		SetInitialReferences();
		enemyMaster.EventEnemyDie += DisableThis;
		enemyMaster.EventEnemyDecreaseHealth += PauseNavMeshAgent;
	}

	void OnDisable()
	{
		enemyMaster.EventEnemyDie -= DisableThis;
		enemyMaster.EventEnemyDecreaseHealth -= PauseNavMeshAgent;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		enemyMaster = GetComponent<EnemyMaster>();
		if (GetComponent<NavMeshAgent>() != null)
		{
			myNavMeshAgent = GetComponent<NavMeshAgent>();
		}
	}

	void PauseNavMeshAgent(int dummy)
	{
		if (myNavMeshAgent != null)
		{
			if (myNavMeshAgent.enabled)
			{
				myNavMeshAgent.ResetPath();
				enemyMaster.isNavPaused = true;
				StartCoroutine(RestartNavMeshAgent());
			}
		}
	}

	IEnumerator RestartNavMeshAgent()
	{
		yield return new WaitForSeconds(pauseTime);
		enemyMaster.isNavPaused = false;
	}

	void DisableThis()
	{
		StopAllCoroutines();
	}
}
}