using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace FPSSystem
{
public class NPCStateAlert : NPCStateInterface
{
	private readonly NPCStatePattern nPC;
	private Transform possibleTarget;
	private RaycastHit hit;
	private Collider[] colliders;
	private Collider[] friendlyColliders;
	private Vector3 targetPosition;
	private Vector3 lookAtTarget;
	private float informRate = 3;
	private float nextInform;
	private float offset = 0.3f;
	private int detectionCount;
	private int lastDetectionCount;

	public NPCStateAlert(NPCStatePattern nPCStatePattern)
	{
		nPC = nPCStatePattern;
	}

	public void UpdateState()
	{
		Look();
	}
	public void ToPatrolState()
	{
		nPC.currentState = nPC.patrolState;
	}
	public void ToAlertState() { }
	public void ToPursueState()
	{
		nPC.currentState = nPC.pursueState;
	}
	public void ToMeleeAttackState() { }
	public void ToRangeAttackState() { }

	void Look()
	{
		colliders = Physics.OverlapSphere(nPC.transform.position, nPC.sightRange, nPC.myEnemyLayers);
		lastDetectionCount = detectionCount;
		foreach (Collider col in colliders)
		{
			lookAtTarget = new Vector3(col.transform.position.x, col.transform.position.y + offset, col.transform.position.z);
			if (Physics.Linecast(nPC.head.position, lookAtTarget, out hit, nPC.sightLayers))
			{
				foreach (string tags in nPC.myEnemyTags)
				{
					if (hit.transform.CompareTag(tags))
					{
						detectionCount++;
						possibleTarget = col.transform;
						break;
					}
				}
			}
		}
		if (detectionCount == lastDetectionCount)
		{
			detectionCount = 0;
		}
		if (detectionCount >= nPC.requiredDetectionCount)
		{
			detectionCount = 0;
			nPC.locationOfInterest = possibleTarget.position;
			nPC.pursueTarget = possibleTarget.root;
			InformNearbyAllies();
			ToPursueState();
		}
		GoToLocationOfInterest();
	}

	void GoToLocationOfInterest()
	{
		nPC.meshRendererFlag.material.color = Color.yellow;
		if (nPC.myNavMeshAgent.enabled && nPC.locationOfInterest != Vector3.zero)
		{
			nPC.myNavMeshAgent.SetDestination(nPC.locationOfInterest);
			nPC.myNavMeshAgent.isStopped = false;
			nPC.nPCMaster.CallEventNPCWalkAnim();
			if (nPC.myNavMeshAgent.remainingDistance <= nPC.myNavMeshAgent.stoppingDistance && !nPC.myNavMeshAgent.pathPending)
			{
				nPC.nPCMaster.CallEventNPCIdleAnim();
				nPC.locationOfInterest = Vector3.zero;
				ToPatrolState();
			}
		}
	}

	void InformNearbyAllies()
	{
		if (Time.time > nextInform)
		{
			nextInform = Time.time + informRate;
			friendlyColliders = Physics.OverlapSphere(nPC.transform.position, nPC.sightRange, nPC.myFriendlyLayers);
			if (friendlyColliders.Length == 0)
			{
				return;
			}
			foreach (Collider ally in friendlyColliders)
			{
				if (ally.transform.root.GetComponent<NPCStatePattern>() != null)
				{
					NPCStatePattern allyPattern = ally.transform.root.GetComponent<NPCStatePattern>();
					if (allyPattern.currentState == allyPattern.patrolState)
					{
						allyPattern.pursueTarget = nPC.pursueTarget;
						allyPattern.locationOfInterest = nPC.pursueTarget.position;
						allyPattern.currentState = allyPattern.alertState;
						allyPattern.nPCMaster.CallEventNPCWalkAnim();
					}
				}
			}
		}
	}
}
}