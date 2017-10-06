using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GunAnimator : MonoBehaviour
{

	private GunMaster gunMaster;
	private Animator myAnimator;

	void OnEnable()
	{
		SetInitialReferences();
		gunMaster.EventPlayerInput += PlayShootAnimation;
		gunMaster.EventNPCInput += NPCPlayShootAnimation;
	}

	void OnDisable()
	{
		gunMaster.EventPlayerInput -= PlayShootAnimation;
		gunMaster.EventNPCInput -= NPCPlayShootAnimation;
	}

	void Start()
	{
		SetInitialReferences();
	}

	void SetInitialReferences()
	{
		gunMaster = GetComponent<GunMaster>();
		if (GetComponent<Animator>() != null)
		{
			myAnimator = GetComponent<Animator>();
		}
	}

	void PlayShootAnimation()
	{
		if (myAnimator != null)
		{
			myAnimator.SetTrigger("Shoot");
		}
	}

	void NPCPlayShootAnimation(float dummy)
	{
		PlayShootAnimation();
	}
}
}