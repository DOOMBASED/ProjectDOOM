using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class ItemAnimator : MonoBehaviour
{

	private ItemMaster itemMaster;

	public Animator myAnimator;

	void OnEnable()
	{
		SetInitialReferences();
		itemMaster.EventObjectThrow += DisableMyAnimator;
		itemMaster.EventObjectPickup += EnableMyAnimator;
	}

	void OnDisable()
	{
		itemMaster.EventObjectThrow -= DisableMyAnimator;
		itemMaster.EventObjectPickup -= EnableMyAnimator;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		itemMaster = GetComponent<ItemMaster>();
	}

	void EnableMyAnimator()
	{
		if (myAnimator != null)
		{
			myAnimator.enabled = true;
		}
	}

	void DisableMyAnimator()
	{
		if (myAnimator != null)
		{
			myAnimator.enabled = false;
		}
	}
}
}