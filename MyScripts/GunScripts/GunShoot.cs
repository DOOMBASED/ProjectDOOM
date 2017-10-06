using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunShoot : MonoBehaviour
{

	private GunMaster gunMaster;
	private Transform myTransform;
	private Transform camTransform;
	private RaycastHit hit;
	private Vector3 startPosition;
	private float offsetFactor = 7;

	public float range = 400;

	void OnEnable()
	{
		SetInitialReferences();
		gunMaster.EventPlayerInput += OpenFire;
		gunMaster.EventSpeedCaptured += SetStartOfShootingPosition;
	}

	void OnDisable()
	{
		gunMaster.EventPlayerInput -= OpenFire;
		gunMaster.EventSpeedCaptured -= SetStartOfShootingPosition;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		gunMaster = GetComponent<GunMaster>();
		myTransform = transform;
		camTransform = myTransform.parent;
	}

	void OpenFire()
	{
		if (Physics.Raycast(camTransform.TransformPoint(startPosition), camTransform.forward, out hit, range))
		{
			if (hit.transform.GetComponent<NPCTakeDamage>() != null)
			{
				gunMaster.CallEventShotEnemy(hit, hit.transform);
			}
			else
			{
				gunMaster.CallEventShotDefault(hit, hit.transform);
			}
		}
	}

	void SetStartOfShootingPosition(float playerSpeed)
	{
		float offset = playerSpeed / offsetFactor;
		startPosition = new Vector3(Random.Range(-offset, offset), Random.Range(-offset, offset), 1);
	}
}
}