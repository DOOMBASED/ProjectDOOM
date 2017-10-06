using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class EnemyNavPursue : MonoBehaviour
{

	private EnemyMaster enemyMaster;
	private NavMeshAgent myNavMeshAgent;
	private float checkRate;
	private float nextCheck;

	void OnEnable()
	{
		SetInitialReferences();
		enemyMaster.EventEnemyDie += DisableThis;
	}

	void OnDisable()
	{
		enemyMaster.EventEnemyDie -= DisableThis;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void Update()
	{
		if (Time.time > nextCheck)
		{
			nextCheck = Time.time + checkRate;
			TryToChaseTarget();
		}
	}

	void SetInitialReferences()
	{
		enemyMaster = GetComponent<EnemyMaster>();
		if (GetComponent<NavMeshAgent>() != null)
		{
			myNavMeshAgent = GetComponent<NavMeshAgent>();
		}
		checkRate = Random.Range(0.1f, 0.2f);
	}

	void TryToChaseTarget()
	{
		if (enemyMaster.myTarget != null && myNavMeshAgent != null && !enemyMaster.isNavPaused)
		{
			myNavMeshAgent.SetDestination(enemyMaster.myTarget.position);
			if (myNavMeshAgent.remainingDistance > myNavMeshAgent.stoppingDistance)
			{
				enemyMaster.CallEventEnemyWalking();
				enemyMaster.isOnRoute = true;
			}
		}
	}

	void DisableThis()
	{
		if (myNavMeshAgent != null)
		{
			myNavMeshAgent.enabled = false;
		}
		this.enabled = false;
	}
}
}