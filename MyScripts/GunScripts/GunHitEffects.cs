using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunHitEffects : MonoBehaviour
{

	private GunMaster gunMaster;

	public GameObject defaultHitEffect;
	public GameObject enemyHitEffect;


	void OnEnable()
	{
		SetInitialReferences();
		gunMaster.EventShotDefault += SpawnDefaultHitEffect;
		gunMaster.EventShotEnemy += SpawnEnemyHitEffect;
	}

	void OnDisable()
	{
		gunMaster.EventShotDefault -= SpawnDefaultHitEffect;
		gunMaster.EventShotEnemy -= SpawnEnemyHitEffect;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		gunMaster = GetComponent<GunMaster>();
	}

	void SpawnDefaultHitEffect(RaycastHit hitPosition, Transform hitTransform)
	{
		if (defaultHitEffect != null)
		{
			Quaternion quatAngle = Quaternion.LookRotation(hitPosition.normal);
			Instantiate(defaultHitEffect, hitPosition.point, quatAngle);
		}
	}

	void SpawnEnemyHitEffect(RaycastHit hitPosition, Transform hitTransform)
	{
		if (enemyHitEffect != null)
		{
			Quaternion quatAngle = Quaternion.LookRotation(hitPosition.normal);
			Instantiate(enemyHitEffect, hitPosition.point, quatAngle);
		}
	}
}
}