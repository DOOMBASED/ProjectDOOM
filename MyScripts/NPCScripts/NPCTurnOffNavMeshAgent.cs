using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class NPCTurnOffNavMeshAgent : MonoBehaviour
{

	private NPCMaster nPCMaster;
	private NavMeshAgent myNavMeshAgent;

	void OnEnable()
	{
		SetInitialReferences();
		nPCMaster.EventNPCDie += TurnOffNavMeshAgent;
	}

	void OnDisable()
	{
		nPCMaster.EventNPCDie -= TurnOffNavMeshAgent;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		nPCMaster = GetComponent<NPCMaster>();
		if (GetComponent<NavMeshAgent>() != null)
		{
			myNavMeshAgent = GetComponent<NavMeshAgent>();
		}
	}

	void TurnOffNavMeshAgent()
	{
		if (myNavMeshAgent != null)
		{
			myNavMeshAgent.enabled = false;
		}
	}
}
}