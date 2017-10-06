using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class EnemyAttack : MonoBehaviour
{

	private EnemyMaster enemyMaster;
	private Transform attackTarget;
	private Transform myTransform;
	private float nextAttack;

	public float attackRate = 1;
	public float attackRange = 3.5f;
	public int attackDamage = 10;

	void OnEnable()
	{
		SetInitialReferences();
		enemyMaster.EventEnemyDie += DisableThis;
		enemyMaster.EventEnemySetNavTarget += SetAttackTarget;
	}

	void OnDisable()
	{
		enemyMaster.EventEnemyDie -= DisableThis;
		enemyMaster.EventEnemySetNavTarget -= SetAttackTarget;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void Update()
	{
		TryToAttack();
	}

	void SetInitialReferences()
	{
		enemyMaster = GetComponent<EnemyMaster>();
		myTransform = transform;
	}

	void SetAttackTarget(Transform targetTransform)
	{
		attackTarget = targetTransform;
	}

	void TryToAttack()
	{
		if (attackTarget != null)
		{
			if (Time.time > nextAttack)
			{
				nextAttack = Time.time + attackRate;
				if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange)
				{
					Vector3 lookAtVector = new Vector3(attackTarget.position.x, myTransform.position.y, attackTarget.position.z);
					myTransform.LookAt(lookAtVector);
					enemyMaster.CallEventEnemyAttack();
					enemyMaster.isOnRoute = false;
				}
			}
		}
	}

	public void OnEnemyAttack()
	{
		if (attackTarget != null)
		{
			if (Vector3.Distance(myTransform.position, attackTarget.position) <= attackRange && attackTarget.GetComponent<PlayerMaster>() != null)
			{
				Vector3 toOther = attackTarget.position - myTransform.position;
				if (Vector3.Dot(toOther, myTransform.forward) > 0.5f)
				{
					attackTarget.GetComponent<PlayerMaster>().CallEventPlayerHealthDecrease(attackDamage);
				}
			}
		}
	}

	void DisableThis()
	{
		this.enabled = false;
	}
}
}