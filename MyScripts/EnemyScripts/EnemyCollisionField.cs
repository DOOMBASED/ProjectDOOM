using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class EnemyCollisionField : MonoBehaviour
{

	private EnemyMaster enemyMaster;
	private Rigidbody rigidbodyStrikingMe;
	private float damageFactor = 0.1f;
	private int damageToApply;

	public float massRequirement = 50;
	public float speedRequirement = 5;

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

	void SetInitialReferences()
	{
		enemyMaster = transform.root.GetComponent<EnemyMaster>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Rigidbody>() != null)
		{
			rigidbodyStrikingMe = other.GetComponent<Rigidbody>();
			if (rigidbodyStrikingMe.mass >= massRequirement && rigidbodyStrikingMe.velocity.sqrMagnitude > speedRequirement * speedRequirement)
			{
				damageToApply = (int)(damageFactor * rigidbodyStrikingMe.mass * rigidbodyStrikingMe.velocity.magnitude);
				enemyMaster.CallEventEnemyDecreaseHealth(damageToApply);
			}
		}
	}

	void DisableThis()
	{
		gameObject.SetActive(false);
	}
}
}