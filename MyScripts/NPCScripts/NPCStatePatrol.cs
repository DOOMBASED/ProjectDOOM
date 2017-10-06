using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FPSSystem
{
public class NPCStatePatrol : NPCStateInterface
{
	private Collider[] colliders;
	private Vector3 lookAtPoint;
	private Vector3 heading;
	private readonly NPCStatePattern nPC;
	private int nextWayPoint;
	private float dotProd;

	public NPCStatePatrol(NPCStatePattern nPCStatePatern)
	{
		nPC = nPCStatePatern;
	}

	public void UpdateState()
	{
		Look();
		Patrol();
	}
	public void ToPatrolState() { }
	public void ToAlertState()
	{
		nPC.currentState = nPC.alertState;
	}
	public void ToPursueState() { }
	public void ToMeleeAttackState() { }
	public void ToRangeAttackState() { }

	void Look()
	{
		colliders = Physics.OverlapSphere(nPC.transform.position, nPC.sightRange / 3, nPC.myEnemyLayers);
		if (colliders.Length > 0)
		{
			VisibilityCalculations(colliders[0].transform);
			if (dotProd > 0)
			{
				AlertStateActions(colliders[0].transform);
				return;
			}
		}
		colliders = Physics.OverlapSphere(nPC.transform.position, nPC.sightRange, nPC.myEnemyLayers);
		foreach (Collider col in colliders)
		{
			RaycastHit hit;
			VisibilityCalculations(col.transform);
			if (Physics.Linecast(nPC.head.position, lookAtPoint, out hit, nPC.sightLayers))
			{
				foreach (string tags in nPC.myEnemyTags)
				{
					if (hit.transform.CompareTag(tags))
					{
						if (dotProd > 0)
						{
							AlertStateActions(col.transform);
							return;
						}
					}
				}
			}
		}
	}

	void Patrol()
	{
		nPC.meshRendererFlag.material.color = Color.green;
		if (nPC.myFollowTarget != null)
		{
			nPC.currentState = nPC.followState;
		}
		if (!nPC.myNavMeshAgent.enabled)
		{
			return;
		}
		if (nPC.waypoints.Length > 0)
		{
			MoveTo(nPC.waypoints[nextWayPoint].position);
			if (HaveIReachedDestination())
			{
				nextWayPoint = (nextWayPoint + 1) % nPC.waypoints.Length;
			}
		}
		else
		{
			if (HaveIReachedDestination())
			{
				StopWalking();
				if (RandomWanderTarget(nPC.transform.position, nPC.sightRange, out nPC.wanderTarget))
				{
					MoveTo(nPC.wanderTarget);
				}
			}
		}
	}

	void AlertStateActions(Transform target)
	{
		nPC.locationOfInterest = target.position;
		ToAlertState();
	}

	void VisibilityCalculations(Transform target)
	{
		lookAtPoint = new Vector3(target.position.x, target.position.y + nPC.offset, target.position.z);
		heading = lookAtPoint - nPC.transform.position;
		dotProd = Vector3.Dot(heading, nPC.transform.forward);
	}

	bool RandomWanderTarget(Vector3 center, float range, out Vector3 result)
	{
		NavMeshHit navHit;

		Vector3 randomPoint = center + Random.insideUnitSphere * nPC.sightRange;
		if (NavMesh.SamplePosition(randomPoint, out navHit, 3.0f, NavMesh.AllAreas))
		{
			result = navHit.position;
			return true;
		}
		else
		{
			result = center;
			return false;
		}
	}

	bool HaveIReachedDestination()
	{
		if (nPC.myNavMeshAgent.remainingDistance <= nPC.myNavMeshAgent.stoppingDistance && !nPC.myNavMeshAgent.pathPending)
		{
			StopWalking();
			return true;
		}
		else
		{
			KeepWalking();
			return false;
		}
	}

	void MoveTo(Vector3 targetPos)
	{
		if (Vector3.Distance(nPC.transform.position, targetPos) > nPC.myNavMeshAgent.stoppingDistance + 1)
		{
			nPC.myNavMeshAgent.SetDestination(targetPos);
			KeepWalking();
		}
	}

	void KeepWalking()
	{
		nPC.myNavMeshAgent.isStopped = false;
		nPC.nPCMaster.CallEventNPCWalkAnim();
	}

	void StopWalking()
	{
		nPC.myNavMeshAgent.isStopped = true;
		nPC.nPCMaster.CallEventNPCIdleAnim();
	}
}
}