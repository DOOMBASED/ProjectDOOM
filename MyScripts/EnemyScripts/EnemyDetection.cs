using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class EnemyDetection : MonoBehaviour
{

	private EnemyMaster enemyMaster;
	private Transform myTransform;
	private RaycastHit hit;
	private float checkRate;
	private float nextCheck;

	public Transform head;
	public LayerMask playerLayer;
	public LayerMask sightLayer;
	public float detectRadius;
	public float stealthRadius = 15;
	public float fieldOfView = 75;


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
		CarryOutDetection();
	}

	void SetInitialReferences()
	{
		enemyMaster = GetComponent<EnemyMaster>();
		myTransform = transform;
		if (head == null)
		{
			head = myTransform;
		}
		checkRate = Random.Range(0.8f, 1.2f);
		fieldOfView = Mathf.Cos(fieldOfView * Mathf.Deg2Rad);
	}

	void CarryOutDetection()
	{
		if (Time.time > nextCheck)
		{
			nextCheck = Time.time + checkRate;
			Collider[] colliders = Physics.OverlapSphere(myTransform.position, detectRadius, playerLayer);
			if (colliders.Length > 0)
			{
				foreach (Collider potentialTargetCollider in colliders)
				{
					if (potentialTargetCollider.CompareTag(GameManagerReferences._playerTag))
					{
						if (CanPotentialTargetBeSeen(potentialTargetCollider.transform))
						{
							break;
						}
					}
				}
			}
			else
			{
				enemyMaster.CallEventEnemyLostTarget();
			}
		}
	}

	bool CanPotentialTargetBeSeen(Transform potentialTarget)
	{
		if (Physics.Linecast(head.position, potentialTarget.position, out hit, sightLayer))
		{
			if (hit.transform == potentialTarget && (Vector3.Dot(head.forward, (potentialTarget.position - head.position).normalized) >= fieldOfView || Vector3.Distance(head.position, potentialTarget.position) < stealthRadius))
			{
				enemyMaster.CallEventEnemySetNavTarget(potentialTarget);
				return true;
			}
			else
			{
				enemyMaster.CallEventEnemyLostTarget();
				return false;
			}
		}
		else
		{
			enemyMaster.CallEventEnemyLostTarget();
			return false;
		}
	}

	void DisableThis()
	{
		this.enabled = false;
	}
}
}