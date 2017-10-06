using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class DestructibleCollisionDetection : MonoBehaviour
{

	private DestructibleMaster destructibleMaster;
	private Rigidbody myRigidbody;

	public float thresholdMass = 50;
	public float thresholdSpeed = 6;

    void OnEnable()
    {
        SetInitialReferences();
    }

    void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		destructibleMaster = GetComponent<DestructibleMaster>();
		if (GetComponent<Rigidbody>() != null)
		{
			myRigidbody = GetComponent<Rigidbody>();
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.contacts.Length > 0)
		{
			if (col.contacts[0].otherCollider.GetComponent<Rigidbody>() != null)
			{
				CollisionCheck(col.contacts[0].otherCollider.GetComponent<Rigidbody>());
			}
			else
			{
				SelfSpeedCheck();
			}
		}
	}

	void CollisionCheck(Rigidbody otherRigidbody)
	{
		if (otherRigidbody.mass > thresholdMass && otherRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed))
		{
			int damage = (int)otherRigidbody.mass;
			destructibleMaster.CallEventDecreaseHealth(damage);
		}
		else
		{
			SelfSpeedCheck();
		}
	}

	void SelfSpeedCheck()
	{
		if (myRigidbody.velocity.sqrMagnitude > (thresholdSpeed * thresholdSpeed))
		{
			int damage = (int)myRigidbody.mass;
			destructibleMaster.CallEventDecreaseHealth(damage);
		}
	}
}
}